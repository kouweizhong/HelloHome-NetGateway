using System;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using System.Collections.Generic;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Commands;

namespace HelloHome.NetGateway.Agents.NodeGateway.Encoders
{
	public class NodeConfigEncoder : MessageEncoder<NodeConfigCommand>
	{
		readonly PinConfigEncoder _pinConfigEncoder;

		public NodeConfigEncoder (PinConfigEncoder pinConfigEncoder)
		{
			this._pinConfigEncoder = pinConfigEncoder;			
		}

		public override byte[] EncodeInternal (NodeConfigCommand message)
		{
			var bytes = new List<Byte> ();
			bytes.Add (2 + 0 << 2);
			bytes.Add(message.NewRfAddress);
			bytes.Add(_pinConfigEncoder.EncodePins(message.Hal1Pin, message.Hal2Pin));
			bytes.Add(_pinConfigEncoder.EncodePins(message.DryPin));
			bytes.Add(_pinConfigEncoder.EncodePins(message.vInTriggerPin, message.vInMeasurePin));
			bytes.Add ((byte)((message.BmpEnable ? 1 : 0) << 1 + (message.SiEnable ? 1 : 0)));
			return bytes.ToArray ();
		}
	}
}

