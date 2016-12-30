using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;

namespace HelloHome.NetGateway.Handlers
{
    public interface IMessageHandler
    {
        IList<OutgoingMessage> Handle(IncomingMessage request);
    }

    public interface IAsyncMessageHandlder
    {
        Task<IList<OutgoingMessage>> HandleAsync(IncomingMessage request);
    }
}