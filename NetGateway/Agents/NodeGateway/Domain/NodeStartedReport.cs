using System;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain
{
	public class NodeStartedReport : Report, ISignedMessage
	{
		public int Major { get; set; }
		public int Minor { get; set; }
		public int OldSignature  { get; set; }
		public long Signature { get; set; }
		public bool NeedNewRfAddress { get; set; }

		public override string ToString ()
		{
			return $"[NodeStartedReport: {Major}, Minor={Minor}, OldSignature={OldSignature}, Signature={Signature}, NeedNewRfAddress={NeedNewRfAddress}]";
		}
	}
}