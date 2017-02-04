using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain.Reports
{
	public class CommentReport : Report
	{
		public string Comment { get; private set; }

		public CommentReport (string comment)
		{
			this.Comment = comment;
		}

		public override string ToString ()
		{
			return $"[CommentReport: Comment={Comment}]";
		}
	}
}

