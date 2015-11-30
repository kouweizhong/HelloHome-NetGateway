using System;

namespace HelloHome.Common.Entities
{
	public class NodeConfiguration
	{
		public virtual int NodeId { get; set; }
		public virtual byte Hal1Pin { get; set; }
		public virtual byte Hal2Pin { get; set; }

		/*
		public byte DryPin { get; set; }

		public byte VinTriggerPin { get; set; }

		public byte VinMeasurePin { get; set; }

		public bool SiEnable { get; set; }

		public bool BmpEnable { get; set; }
		*/
	}
}

