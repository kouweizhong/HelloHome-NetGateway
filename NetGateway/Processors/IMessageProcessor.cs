using System;
using NetHhGateway.Agents.NodeGateway.Domain;
using NetHhGateway.Entities;
using System.Collections.Generic;

namespace NetHhGateway.Processors
{
	public interface IMessageProcessor
	{
		bool CanProcess (IncomingMessage message);

		IList<OutgoingMessage> Process (IncomingMessage message);
	}

}

