using System;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Commands;

namespace HelloHome.NetGateway.Agents.NodeGateway.Encoders
{
	public class PinConfigEncoder
	{
		public PinConfigEncoder ()
		{
		}

		public byte EncodePins(PinConfig pin1, PinConfig pin2)
		{
			byte e = 0;
			e += (byte)(pin1.Mode == PinMode.Digital ? 0 : 1);
			if (pin1.PinNumber > 7)
				throw new ApplicationException ("Pin number cannot exceed 7");
			e += (byte)(pin1.PinNumber << 1);
			e += (byte)((pin2.Mode == PinMode.Digital ? 0 : 1) << 4);
			if (pin2.PinNumber > 7)
				throw new ApplicationException ("Pin number cannot exceed 7");
			e += (byte)(pin2.PinNumber << 5);
			return e;
		}

		public byte EncodePins(PinConfig pin1)
		{
			byte e = 0;
			e += (byte)(pin1.Mode == PinMode.Digital ? 0 : 1);
			if (pin1.PinNumber > 7)
				throw new ApplicationException ("Pin number cannot exceed 7");
			e += (byte)(pin1.PinNumber << 1);
			return e;
		}
	}
}

