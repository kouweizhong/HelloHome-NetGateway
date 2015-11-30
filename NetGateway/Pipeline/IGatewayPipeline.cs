using System;
using HelloHome.NetGateway.Agents.NodeGateway;
using System.Collections.Generic;
using HelloHome.NetGateway.Processors;
using System.Linq;
using System.Threading;
using HelloHome.Common.Entities;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using System.Threading.Tasks;
using HelloHome.NetGateway.Logic.RfNodeIdGenerationStrategy;
using log4net;

namespace HelloHome.NetGateway.Pipeline
{
	public interface IGatewayPipeline
	{
		void Process (ProcessingContext context);
	}

}

