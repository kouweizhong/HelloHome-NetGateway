using HelloHome.NetGateway.MessageChannel.Domain.Base;

namespace HelloHome.NetGateway.MessageChannel.Serializer
{
    public interface IMessageSerializer
    {
        byte[] Serialize(Message message);
        Message Deserialize(byte[] bytes);
    }
}