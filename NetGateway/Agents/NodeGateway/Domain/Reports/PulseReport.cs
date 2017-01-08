using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain.Reports
{
	public class PulseReport : Report
	{
		public int SubNode { get; set; }
		public int NewPulses { get; set; }

		public override string ToString ()
		{
			return string.Format ("[PulseReport: Node={2}, SubNode={0}, NewPulses={1}]", SubNode, NewPulses, FromNodeId);
		}
	}
}

