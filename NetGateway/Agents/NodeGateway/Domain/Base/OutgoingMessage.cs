using System.Runtime.Serialization;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain.Base
{
    [DataContract]
    public abstract class OutgoingMessage : Message
	{
	    [DataMember]
	    public int ToNodeId { get; set; }
	}
}

