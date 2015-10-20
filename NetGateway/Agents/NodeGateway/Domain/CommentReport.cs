using System;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain
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
			return string.Format ("[CommentReport: Comment={0}]", Comment);
		}
	}
}

