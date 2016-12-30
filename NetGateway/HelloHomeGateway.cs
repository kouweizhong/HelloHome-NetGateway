using System;
using System.Threading;
using System.Threading.Tasks;
using HelloHome.NetGateway.Agents.NodeGateway;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.Handlers;
using NLog;

namespace HelloHome.NetGateway
{
    public class NodeGateway
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly INodeMessageChannel _nodeMessageChannel;
        private readonly IMessageHandlerFactory _handlerFactory;

        public NodeGateway(INodeMessageChannel nodeMessageChannel, IMessageHandlerFactory handlerFactory)
        {
            _nodeMessageChannel = nodeMessageChannel;
            _handlerFactory = handlerFactory;
        }

        public async Task RunLoop(CancellationToken cToken)
        {
            while (!cToken.IsCancellationRequested)
            {
                var msg = await _nodeMessageChannel.ReadAsync(cToken);
                if (msg != null)
                    Process(msg, cToken);
            }
        }

        private async void Process(IncomingMessage msg, CancellationToken cToken)
        {
            var handler = _handlerFactory.Create(msg);
            try
            {
                var responses = handler.Handle(msg);
                if (responses == null)
                    return;

                foreach (var r in responses)
                    await _nodeMessageChannel.SendAsync(r, cToken);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            finally
            {
                _handlerFactory.Release(handler);
            }
        }
    }
}

