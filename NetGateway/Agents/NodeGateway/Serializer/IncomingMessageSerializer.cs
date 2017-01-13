using System;
using System.Collections.Generic;
using System.Linq;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;

namespace HelloHome.NetGateway.Agents.NodeGateway.Serializer
{
    public abstract class IncomingMessageSerializer<T> : IMessageSerializer where T : IncomingMessage
    {
        public byte[] Serialize(Message message)
        {
            return SerializeInternal(message as T).ToArray();
        }

        protected virtual IEnumerable<byte> SerializeInternal(T msg)
        {
            throw new NotImplementedException("Not implemented as it is not mandatory (incomng message)");
        }

        public Message Deserialize(byte[] bytes)
        {
            return DeserializeInternal(bytes);
        }

        protected abstract T DeserializeInternal(byte[] bytes);
    }
}