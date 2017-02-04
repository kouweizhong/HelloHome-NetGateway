using System.Runtime.Serialization;
using HelloHome.NetGateway.MessageChannel.Domain.Base;

namespace HelloHome.NetGateway.MessageChannel.Domain.Reports
{
    [DataContract]
    public class NodeStartedReport : Report, ISignedMessage
	{
	    [DataMember]
	    public int Major { get; set; }
	    [DataMember]
	    public int Minor { get; set; }
	    [DataMember]
	    public int OldSignature  { get; set; }
	    [DataMember]
	    public long Signature { get; set; }
	    [DataMember]
	    public bool NeedNewRfAddress { get; set; }

		public override string ToString ()
		{
			return $"[NodeStartedReport: From={FromNodeId}, Major={Major}, Minor={Minor}, OldSignature={OldSignature}, Signature={Signature}, NeedNewRfAddress={NeedNewRfAddress}]";
		}
	}
}