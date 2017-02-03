using System.Runtime.Serialization;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain.Base
{
    public abstract class OutgoingMessage : Message
	{

	    public byte ToNodeId { get; set; }
	}
}

