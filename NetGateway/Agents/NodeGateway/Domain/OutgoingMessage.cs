using System;

namespace NetHhGateway.Agents.NodeGateway.Domain
{
	public abstract class OutgoingMessage : Message
	{
		public int ToNodeId { get; set; }
	}
}

