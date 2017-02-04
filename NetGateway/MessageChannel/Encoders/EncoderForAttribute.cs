using System;
using HelloHome.NetGateway.MessageChannel.Domain.Base;

namespace HelloHome.NetGateway.MessageChannel.Encoders
{
    public class EncoderForAttribute : Attribute
    {
        private readonly Type _messageType;

        public EncoderForAttribute(Type messageType)
        {
            if (!typeof(OutgoingMessage).IsAssignableFrom(messageType))
                throw new Exception($"{nameof(messageType)} should inherits OutgoingMessage");
            _messageType = messageType;
        }
    }
}