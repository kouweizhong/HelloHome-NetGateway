using System;

namespace HelloHome.NetGateway.Agents.NodeGateway.Serializer
{
    public class SerializerForAttribute : Attribute
    {
        public Type MessageType { get; }
        public byte Identifier { get; }

        public SerializerForAttribute(Type messageType, byte identifier)
        {
            MessageType = messageType;
            Identifier = identifier;
        }
    }
}