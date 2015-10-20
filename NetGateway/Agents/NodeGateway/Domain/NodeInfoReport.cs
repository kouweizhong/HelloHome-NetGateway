using System;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain
{
	public class NodeInfoReport : Report
	{
		public int SendErrorCount { get; set; }
		public int StartCount { get; set; }
		public float? Voltage { get; set; }

		public override string ToString ()
		{
			return string.Format ("[NodeInfoReport: NodeId={2}, SendErrorCount={0}, Voltage={1}]", SendErrorCount, Voltage, FromNodeId);
		}
	}
}

