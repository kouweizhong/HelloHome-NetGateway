using System;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain
{
	public class IncomingMessage : Message
	{
		public byte FromNodeId { get; set; }
		public int Rssi { get; set; }
	}
}

