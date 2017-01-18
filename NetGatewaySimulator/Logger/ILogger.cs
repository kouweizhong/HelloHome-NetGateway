using System;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;

namespace NetGatewaySimulator.Logger
{
    public interface ILogger
    {
        void IncomingMessage(IncomingMessage msg);
        void Outgoingmessage(OutgoingMessage msg);
        void Exception(Exception ex);
        void Info(string info);
    }
}