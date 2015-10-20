using System;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain
{
	public class NodeConfigCommand : OutgoingMessage
	{
		public long signature { get; set; }

		public byte NewRfAddress { get; set; }

		public PinConfig Hal1Pin { get; set; }

		public PinConfig Hal2Pin { get; set; }

		public PinConfig DryPin { get; set; }

		public PinConfig vInTriggerPin { get; set; }

		public PinConfig vInMeasurePin { get; set; }

		public bool SiEnable { get; set; }

		public bool BmpEnable { get; set; }
	}


}

