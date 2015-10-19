using System;

namespace NetHhGateway.Entities
{
	public class NodeConfiguration
	{
		public int NodeId { get; set; }
		public byte Hal1Pin { get; set; }

		public byte Hal2Pin { get; set; }

		/*
		public byte DryPin { get; set; }

		public byte VinTriggerPin { get; set; }

		public byte VinMeasurePin { get; set; }

		public bool SiEnable { get; set; }

		public bool BmpEnable { get; set; }
		*/
	}
}

