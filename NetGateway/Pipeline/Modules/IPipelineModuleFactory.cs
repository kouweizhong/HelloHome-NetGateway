using System;

namespace HelloHome.NetGateway.Pipeline
{
	public interface IPipelineModuleFactory
	{
		TP Create<TP> () where TP : IPipelineModule;
		void Release(IPipelineModule module);
	}
}

