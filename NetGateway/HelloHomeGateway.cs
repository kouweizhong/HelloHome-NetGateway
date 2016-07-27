using System;
using HelloHome.NetGateway.Pipeline;
using HelloHome.NetGateway.Agents.NodeGateway;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.Common.Entities;
using log4net;

namespace HelloHome.NetGateway
{
	public class HelloHomeGateway
	{
		static readonly ILog log = LogManager.GetLogger(typeof(HelloHomeGateway).Name);
		readonly INodeGatewayAgent _nodeGatewayAgent;
		readonly IGatewayPipelineFactory _pipelineFactory;

		public HelloHomeGateway (IEMonCmsUpdater emonCmsUpdater, INodeGatewayAgent nodeGatewayAgent, IGatewayPipelineFactory pipelineFactory)
		{
			_pipelineFactory = pipelineFactory;
			_nodeGatewayAgent = nodeGatewayAgent;

			nodeGatewayAgent.OnMessageReceived += MessageReceived; 
			nodeGatewayAgent.Start ();
		}

		public void MessageReceived(object sender, IncomingMessage message)
		{
			var processingContext = new ProcessingContext (message);
			var pipeline = _pipelineFactory.Create ();
			try {
				pipeline.Process (processingContext);
				foreach (var om in processingContext.Responses)
					_nodeGatewayAgent.Send (om);				
			}
			catch(Exception ex){
				log.Error (ex.Message);
			}
			finally{
				_pipelineFactory.Release (pipeline);
			}
		}
	}
}

