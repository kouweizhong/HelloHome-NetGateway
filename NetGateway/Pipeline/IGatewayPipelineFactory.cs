using System;

namespace HelloHome.NetGateway.Pipeline
{
	public interface IGatewayPipelineFactory
	{
		IGatewayPipeline Create();
		void Release(IGatewayPipeline pipeline);
	}
}

