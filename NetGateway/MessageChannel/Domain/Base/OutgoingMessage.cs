namespace HelloHome.NetGateway.MessageChannel.Domain.Base
{
    public abstract class OutgoingMessage : Message
	{

	    public byte ToNodeId { get; set; }
	}
}

