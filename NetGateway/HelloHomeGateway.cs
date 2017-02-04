using System;
using System.Threading;
using System.Threading.Tasks;
using HelloHome.NetGateway.Handlers;
using HelloHome.NetGateway.Handlers.Factory;
using HelloHome.NetGateway.MessageChannel;
using HelloHome.NetGateway.MessageChannel.Domain.Base;
using NLog;

namespace HelloHome.NetGateway
{
    public class NodeGateway
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly Logger MessageLog = LogManager.GetLogger("MessageLog");
        private readonly INodeMessageChannel _nodeMessageChannel;
        private readonly IMessageHandlerFactory _handlerFactory;

        public NodeGateway(INodeMessageChannel nodeMessageChannel, IMessageHandlerFactory handlerFactory)
        {
            _nodeMessageChannel = nodeMessageChannel;
            _handlerFactory = handlerFactory;
        }

        public async Task RunLoopAsync(CancellationToken cToken)
        {
            //var tasks = new List<Task>(10);
            while (!cToken.IsCancellationRequested)
            {
                //while(tasks.Count > 10)
                //    await Task.WhenAny(tasks);
                //tasks.Add(RunOnceAsync(cToken));
                await RunOnceAsync(cToken);
            }
            //await Task.WhenAll(tasks);
        }

        public async Task RunOnceAsync(CancellationToken cToken)
        {
            Logger.Debug("Will read from channel {0}", _nodeMessageChannel.GetHashCode());
            var msg = await _nodeMessageChannel.ReadAsync(cToken);
            if (msg != null)
            {
                MessageLog.Debug("IN:{0}", msg);
                Logger.Debug("Message of type {0} received from node {1} on channel {2}", msg.GetType().Name, msg.FromNodeId, _nodeMessageChannel.GetHashCode());
                await ProcessAsync(msg, cToken);
            }
            else
            {
                Logger.Debug("Nothing was found on the channel {0}", _nodeMessageChannel.GetHashCode());
            }
        }

        private async Task ProcessAsync(IncomingMessage msg, CancellationToken cToken)
        {
            IMessageHandler handler = null;
            try
            {
                handler = _handlerFactory.Create(msg);
                Logger.Debug("Handler {0} found for request {1}", handler.GetType().Name, msg.GetType().Name);
                var responses = await handler.HandleAsync(msg, cToken);
                if (responses == null)
                    return;

                Logger.Debug("{0} messages to send back", responses.Count);
                foreach (var r in responses)
                {
                    await _nodeMessageChannel.SendAsync(r, cToken);
                    MessageLog.Debug("OUT:{0}", r);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            finally
            {
                if(handler != null)
                    _handlerFactory.Release(handler);
            }
        }
    }
}