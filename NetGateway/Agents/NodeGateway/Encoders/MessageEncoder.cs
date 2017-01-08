using System;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;

namespace HelloHome.NetGateway.Agents.NodeGateway.Encoders
{
	public abstract class MessageEncoder<TMessage> : IMessageEncoder where TMessage : OutgoingMessage
	{
		#region IMessageEncoder implementation

		public bool CanEncode (Message message)
		{
			return message is TMessage;
		}

		public byte[] Encode (Message message)
		{
			return EncodeInternal (message as TMessage);
		}

		#endregion

		public abstract byte[] EncodeInternal (TMessage message);
	}
}

