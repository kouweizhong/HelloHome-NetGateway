using System;

namespace NetHhGateway.Agents.NodeGateway.Domain
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

