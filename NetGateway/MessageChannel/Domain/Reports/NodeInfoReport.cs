using HelloHome.NetGateway.MessageChannel.Domain.Base;

namespace HelloHome.NetGateway.MessageChannel.Domain.Reports
{
	public class NodeInfoReport : Report
	{
		public int SendErrorCount { get; set; }
		public int StartCount { get; set; }
		public float? Voltage { get; set; }

		public override string ToString ()
		{
			return $"[NodeInfoReport: NodeId={FromNodeId}, SendErrorCount={SendErrorCount}, Voltage={Voltage}]";
		}
	}
}

