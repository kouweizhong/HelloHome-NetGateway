using System;

namespace NetHhGateway.Agents.NodeGateway.Domain
{
	public class AtticEnergyReport : Report
	{
		public int GazPulses { get; set; }
		public int WaterPulses { get; set; }
		public float Temperature { get; set; }
		public float Humidity { get; set; }
		public float BatteryLevel { get; set; }
	}
}

