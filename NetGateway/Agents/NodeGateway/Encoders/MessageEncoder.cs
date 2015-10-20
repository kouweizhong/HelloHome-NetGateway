using System;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;

namespace HelloHome.NetGateway.Agents.NodeGateway.Encoders
{
	public abstract class MessageEncoder<TMessage> : IMessageEncoder where TMessage : OutgoingMessage
	{
		#region IMessageEncoder implementation

		public bool CanEncode (HelloHome.NetGateway.Agents.NodeGateway.Domain.Message message)
		{
			return message is TMessage;
		}

		public byte[] Encode (HelloHome.NetGateway.Agents.NodeGateway.Domain.Message message)
		{
			return EncodeInternal (message as TMessage);
		}

		#endregion

		public abstract byte[] EncodeInternal (TMessage message);
	}
}

