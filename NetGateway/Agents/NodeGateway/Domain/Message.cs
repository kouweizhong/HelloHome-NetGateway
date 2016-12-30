using System;
using System.Runtime.Serialization;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain
{
    [DataContract]
    [KnownType(typeof(IncomingMessage))]
    [KnownType(typeof(Report))]
    [KnownType(typeof(NodeStartedReport))]
    [KnownType(typeof(OutgoingMessage))]
    [KnownType(typeof(NodeConfigCommand))]
    public abstract class Message
	{
	}
}

