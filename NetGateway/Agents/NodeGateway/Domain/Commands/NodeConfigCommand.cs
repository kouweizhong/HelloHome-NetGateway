using System.Runtime.Serialization;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain.Commands
{
	[DataContract]
    public class NodeConfigCommand : OutgoingMessage
	{
		[DataMember]
	    public long Signature { get; set; }

	    [DataMember]
	    public byte NewRfAddress { get; set; }

	    [DataMember]
	    public byte Hal1Pin { get; set; }

	    [DataMember]
	    public byte Hal2Pin { get; set; }

	    [DataMember]
	    public byte DryPin { get; set; }

	    [DataMember]
	    public byte VInTriggerPin { get; set; }

	    [DataMember]
	    public byte VInMeasurePin { get; set; }

	    [DataMember]
	    public bool SiEnable { get; set; }

	    [DataMember]
	    public bool BmpEnable { get; set; }
	}


}

