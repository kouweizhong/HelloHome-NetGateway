using System;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.Common.Entities;
using System.Collections.Generic;

namespace HelloHome.NetGateway.Processors
{
	public interface IMessageProcessor
	{
		bool CanProcess (IncomingMessage message);

		IList<OutgoingMessage> Process (IncomingMessage message);
	}

}

