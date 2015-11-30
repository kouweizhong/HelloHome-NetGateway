using System;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.Common.Entities;
using System.Collections.Generic;
using HelloHome.NetGateway.Pipeline;

namespace HelloHome.NetGateway.Processors
{
	public interface IMessageProcessor
	{
		void Process (ProcessingContext ctx);
	}

}

