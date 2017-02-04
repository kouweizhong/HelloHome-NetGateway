using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;

namespace HelloHome.NetGateway.Agents.NodeGateway.Serializer
{
    public interface ISerializerFactory
    {
        //Incoming messages
        IMessageSerializer Create(byte identifier);
        IncomingMessageSerializer<T> Create<T>(IncomingMessage msg) where T : IncomingMessage;

        //Outgoing Messages
        IMessageSerializer Create(Message msg);
        OutgoingMessageSerializer<T> Create<T>(OutgoingMessage msg) where T : OutgoingMessage;

        void Release(IMessageSerializer serializer);
    }
}