using System;
using NetHhGateway.Agents.NodeGateway.Domain;

namespace NetHhGateway.Agents.NodeGateway.Encoders
{
	public abstract class MessageEncoder<TMessage> : IMessageEncoder where TMessage : OutgoingMessage
	{
		#region IMessageEncoder implementation

		public bool CanEncode (NetHhGateway.Agents.NodeGateway.Domain.Message message)
		{
			return message is TMessage;
		}

		public byte[] Encode (NetHhGateway.Agents.NodeGateway.Domain.Message message)
		{
			return EncodeInternal (message as TMessage);
		}

		#endregion

		public abstract byte[] EncodeInternal (TMessage message);
	}
}

