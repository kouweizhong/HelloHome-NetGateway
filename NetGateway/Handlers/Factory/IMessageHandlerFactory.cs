using HelloHome.NetGateway.MessageChannel.Domain.Base;

namespace HelloHome.NetGateway.Handlers.Factory
{
    public interface IMessageHandlerFactory
    {
        IMessageHandler Create(IncomingMessage request);
        void Release(IMessageHandler messageHandler);
    }
}