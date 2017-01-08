using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Castle.Facilities.TypedFactory;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;

namespace HelloHome.NetGateway.Agents.NodeGateway
{
    [ServiceContract]
    public interface IMessageReader
    {
        [OperationContract]
        void Read(Message message);
    }

    public class NodeMessageWcfChannel : INodeMessageChannel, IMessageReader, IDisposable
    {
        private readonly IMessageReader _remoteReader;
        readonly ConcurrentQueue<Message> _messageQueue = new ConcurrentQueue<Message>();
        private readonly ServiceHost _serviceHost;

        public NodeMessageWcfChannel(IMessageReader remoteReader)
        {
            _remoteReader = remoteReader;
            _serviceHost = new ServiceHost(this, new Uri($"tcp://localhost:7070"));
            _serviceHost.Open();
        }

        public Task SendAsync(OutgoingMessage message, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            Task.Factory.StartNew(() =>
            {
                try
                {
                    _remoteReader.Read(message);
                    tcs.SetResult(true);
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            }, cancellationToken);
            return tcs.Task;
        }

        public async Task<IncomingMessage> ReadAsync(CancellationToken cancelationToken)
        {
            Message msg;
            while (!_messageQueue.TryDequeue(out msg))
                await Task.Delay(50, cancelationToken);
            if (msg is IncomingMessage)
                return msg as IncomingMessage;
            throw new Exception("Only incoming messages are expected from this side");
        }

        public void Read(Message message)
        {
            _messageQueue.Enqueue(message);
        }

        public void Dispose()
        {
            if(_serviceHost.State == CommunicationState.Opened)
                _serviceHost.Close();
        }
    }
}