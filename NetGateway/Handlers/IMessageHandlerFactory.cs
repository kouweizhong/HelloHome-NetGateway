using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.MessageChannel.Domain.Base;

namespace HelloHome.NetGateway.Handlers
{
    public interface IMessageHandlerFactory
    {
        IMessageHandler Create(IncomingMessage request);
        void Release(IMessageHandler messageHandler);
    }
}