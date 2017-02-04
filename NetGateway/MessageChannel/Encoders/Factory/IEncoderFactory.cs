using HelloHome.NetGateway.MessageChannel.Domain.Base;

namespace HelloHome.NetGateway.MessageChannel.Encoders.Factory
{
    public interface IEncoderFactory
    {
        IMessageEncoder Create<T>(T outgoingMessage) where T : OutgoingMessage;
        void Release(IMessageEncoder encoder);
    }
}