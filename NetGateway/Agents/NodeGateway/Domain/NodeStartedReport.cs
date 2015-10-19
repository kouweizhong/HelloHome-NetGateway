using System;

namespace NetHhGateway.Agents.NodeGateway.Domain
{
	public class NodeStartedReport : Report
	{
		public int Major { get; set; }
		public int Minor { get; set; }
		public int Signature { get; set; }
		public bool NeedNewRfAddress { get; set; }
	}
}

