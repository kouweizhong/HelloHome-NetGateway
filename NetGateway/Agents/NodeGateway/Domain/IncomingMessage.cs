using System;
using System.Runtime.Serialization;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain
{
    [DataContract]
	public class IncomingMessage : Message
	{
		[DataMember]
	    public byte FromNodeId { get; set; }
	    [DataMember]
	    public int Rssi { get; set; }
	}
}

