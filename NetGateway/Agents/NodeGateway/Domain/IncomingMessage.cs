using System;

namespace NetHhGateway.Agents.NodeGateway.Domain
{
	public class IncomingMessage : Message
	{
		public byte FromNodeId { get; set; }
	}
}

