using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Threading.Tasks;
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
        readonly IMessageHandlerFactory _handlerFactory;

        public HelloHomeGateway(INodeGatewayAgent nodeGatewayAgent, IMessageHandlerFactory handlerFactory)
        {
            _handlerFactory = handlerFactory;
            _nodeGatewayAgent = nodeGatewayAgent;

            nodeGatewayAgent.OnMessageReceived += MessageReceived;
            nodeGatewayAgent.Start();
        }

        public void MessageReceived(object sender, IncomingMessage message)
        {
			IMessageHandler handler = null;
			try {
				handler = _handlerFactory.Create (message);
				var outMessages = handler.Handle (message);
				if (outMessages != null)
					foreach (var om in outMessages)
						_nodeGatewayAgent.Send (om);
			} catch (Exception) {
				throw;
			} finally {
				if (handler != null)
					_handlerFactory.Dispose (handler);
			}
        }
    }

	public class QueueBasedGateway
	{ 
        readonly INodeGatewayAgent _nodeGatewayAgent;
        readonly IMessageHandlerFactory _handlerFactory;
		readonly ConcurrentQueue<IncomingMessage> messageQueue = new ConcurrentQueue<IncomingMessage>();

		public QueueBasedGateway(INodeGatewayAgent nodeGatewayAgent, IMessageHandlerFactory handlerFactory)
		{
			_handlerFactory = handlerFactory;
			_nodeGatewayAgent = nodeGatewayAgent;

			nodeGatewayAgent.OnMessageReceived += (sender, message) => messageQueue.Enqueue(message);;
			nodeGatewayAgent.Start();
		}

		public async Task<int> ProcessMessages()
		{
			IncomingMessage incomingMessage = null;
			int numberOfMessagesProcessed = 0;
			if (messageQueue.TryDequeue(out incomingMessage))
			{
				IMessageHandler handler = null;
				try
				{
					handler = _handlerFactory.Create(incomingMessage);
					var outMessages = handler.Handle(incomingMessage);
					if (outMessages != null)
						foreach (var om in outMessages)
							_nodeGatewayAgent.Send(om);
				}
				catch (Exception)
				{
					throw;
				}
				finally
				{
					if (handler != null)
						_handlerFactory.Dispose(handler);
				}

				numberOfMessagesProcessed++;
			}
			else
			{
				await Task.Delay(TimeSpan.FromMilliseconds(100));
			}
			return numberOfMessagesProcessed;
		}
	}
}

