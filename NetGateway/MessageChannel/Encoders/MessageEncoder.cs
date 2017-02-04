using HelloHome.NetGateway.MessageChannel.Domain.Base;

namespace HelloHome.NetGateway.MessageChannel.Encoders
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

