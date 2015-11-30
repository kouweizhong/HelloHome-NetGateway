using System;

namespace HelloHome.NetGateway.Pipeline
{
	public interface IPipelineModule
	{
		IPipelineModule Process (ProcessingContext context, IPipelineModule next);
	}
}

