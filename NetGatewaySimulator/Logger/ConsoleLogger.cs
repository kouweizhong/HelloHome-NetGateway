using System;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;

namespace NetGatewaySimulator.Logger
{
    public class ConsoleLogger : ILogger
    {
        public void IncomingMessage(IncomingMessage msg)
        {
        }

        public void Outgoingmessage(OutgoingMessage msg)
        {
        }

        public void Exception(Exception ex)
        {
        }

        public void Info(string info)
        {
        }
    }
}