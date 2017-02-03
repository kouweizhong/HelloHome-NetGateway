using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain.Reports
{
	public class PulseReport : Report
	{
		public int SubNode { get; set; }
		public int NewPulses { get; set; }

		public override string ToString ()
		{
			return $"[PulseReport: Node={FromNodeId}, SubNode={SubNode}, NewPulses={NewPulses}]";
		}
	}
}

