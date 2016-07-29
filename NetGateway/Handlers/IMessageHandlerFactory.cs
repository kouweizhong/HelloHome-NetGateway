using HelloHome.NetGateway.Agents.NodeGateway.Domain;

namespace HelloHome.NetGateway.Handlers
{
    public interface IMessageHandlerFactory
    {
        IMessageHandler Create(IncomingMessage request);
        void Dispose(IMessageHandler messageHandler);
    }
}