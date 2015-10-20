using System;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain
{
	public abstract class OutgoingMessage : Message
	{
		public int ToNodeId { get; set; }
	}
}

