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
			return string.Format ("[EnvironmentalInfoReport: NodeId={3}, Temperature={0}, Humidity={1}, Pressure={2}]", Temperature, Humidity, Pressure, FromNodeId);
		}
	}
}

