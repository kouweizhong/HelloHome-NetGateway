using System;
using System.Collections.Generic;
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

        public async Task RunLoopAsync(CancellationToken cToken)
        {
            while (!cToken.IsCancellationRequested)
                await RunOnceAsync(cToken);
        }

        public async Task RunOnceAsync(CancellationToken cToken, bool awaitProcess = false)
        {
            var msg = await _nodeMessageChannel.ReadAsync(cToken);
            if (msg != null)
            {
                Logger.Debug("Message of type {0} received from node {1}", msg.GetType().Name, msg.FromNodeId);
                var pt = ProcessAsync(msg, cToken);
                if (awaitProcess)
                    await pt;
            }
        }

        private async Task ProcessAsync(IncomingMessage msg, CancellationToken cToken)
        {
            var handler = _handlerFactory.Create(msg);
            try
            {
                var responses = await handler.HandleAsync(msg);
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