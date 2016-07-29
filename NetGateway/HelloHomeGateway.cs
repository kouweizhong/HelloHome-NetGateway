using System;
using System.ComponentModel;
using HelloHome.NetGateway.Agents.NodeGateway;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.Handlers;
using NLog;

namespace HelloHome.NetGateway
{
    public class HelloHomeGateway
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        readonly INodeGatewayAgent _nodeGatewayAgent;
        private readonly IMessageHandlerFactory _handlerFactory;

        public HelloHomeGateway(IEMonCmsUpdater emonCmsUpdater, INodeGatewayAgent nodeGatewayAgent, IMessageHandlerFactory handlerFactory)
        {
            _handlerFactory = handlerFactory;
            _nodeGatewayAgent = nodeGatewayAgent;

            nodeGatewayAgent.OnMessageReceived += MessageReceived;
            nodeGatewayAgent.Start();
        }

        public void MessageReceived(object sender, IncomingMessage message)
        {
            var handler = _handlerFactory.Create(message);
            try
            {
                var outMessages = handler.Handle(message);
                if(outMessages != null)
                    foreach (var om in outMessages)
                        _nodeGatewayAgent.Send(om);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
                _handlerFactory.Dispose(handler);
            }
        }
    }
}

