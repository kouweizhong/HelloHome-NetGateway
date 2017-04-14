using System;
using HelloHome.NetGateway.MessageChannel.Domain.Base;

namespace HelloHome.NetGateway.MessageChannel.Encoders.Factory
{
    public class EncoderForAttribute : Attribute
    {
        public Type MessageType { get; }

        public EncoderForAttribute(Type messageType)
        {
            if (!typeof(OutgoingMessage).IsAssignableFrom(messageType))
                throw new Exception($"{nameof(messageType)} should inherits OutgoingMessage");
            MessageType = messageType;
        }
    }
}