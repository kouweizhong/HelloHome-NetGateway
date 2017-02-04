using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;

namespace HelloHome.NetGateway.Agents.NodeGateway.Serializer
{
    public interface IMessageSerializer
    {
        byte[] Serialize(Message message);
        Message Deserialize(byte[] bytes);
    }
}