using System;
using System.Collections.Generic;
using System.Linq;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;

namespace HelloHome.NetGateway.Agents.NodeGateway.Serializer
{
    public abstract class OutgoingMessageSerializer<T> : IMessageSerializer where T : OutgoingMessage
    {
        public byte[] Serialize(Message message)
        {
            return SerializeInternal(message as T).ToArray();
        }
        
        protected abstract IEnumerable<byte> SerializeInternal(T msg);

        public Message Deserialize(byte[] bytes)
        {
            return DeserializeInternal(bytes);
        }

        protected virtual T DeserializeInternal(byte[] bytes)
        {
            throw new NotImplementedException("Not implemented as it is not mandatory (outgoing message)");
        }
    }
}