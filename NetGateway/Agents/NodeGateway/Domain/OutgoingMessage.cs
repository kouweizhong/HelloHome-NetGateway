using System;
using System.Runtime.Serialization;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain
{
    [DataContract]
    public abstract class OutgoingMessage : Message
	{
	    [DataMember]
	    public int ToNodeId { get; set; }
	}
}

