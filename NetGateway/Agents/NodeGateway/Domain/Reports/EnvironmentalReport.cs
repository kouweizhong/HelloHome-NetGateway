using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain.Reports
{
	public class EnvironmentalReport : Report
	{
		public float Temperature { get; set; }

		public float Humidity { get; set; }

		public int Pressure { get; set; }

		public override string ToString ()
		{
			return $"[EnvironmentalInfoReport: NodeId={FromNodeId}, Temperature={Temperature}, Humidity={Humidity}, Pressure={Pressure}]";
		}
	}
}

