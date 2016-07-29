using System.Collections;
using System.Collections.Generic;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;

namespace HelloHome.NetGateway.Handlers
{
    public interface IMessageHandler
    {
        IList<OutgoingMessage> Handle(IncomingMessage request);
    }
}