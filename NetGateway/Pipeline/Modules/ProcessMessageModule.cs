using System;
using HelloHome.NetGateway.Processors;
using HelloHome.Common.Entities;

namespace HelloHome.NetGateway.Pipeline
{
	public class ProcessMessageModule : IPipelineModule
	{
		readonly IMessageProcessorFactory _processorFactory;

		public ProcessMessageModule (IMessageProcessorFactory processorFactory)
		{
			_processorFactory = processorFactory;
		}

		#region IPipelineModule implementation

		public IPipelineModule Process (ProcessingContext context, IPipelineModule next)
		{
			var processor = _processorFactory.Create (context.IncomingMessage);
			processor.Process (context);
			_processorFactory.Release (processor);
			return next;
		}

		#endregion
	}
}

