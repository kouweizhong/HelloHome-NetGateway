using System.Text;
using System.Linq;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Reports;

namespace HelloHome.NetGateway.Agents.NodeGateway.Parsers
{
	public class CommentParser : IMessageParser
	{
		private readonly Encoding _encoding = Encoding.ASCII;

		#region IMessageParser implementation
		public bool CanParse (byte[] record)
		{
			return record [0] == '/' && record [1] == '/';
		}
		public Report Parse (byte[] record)
		{
			return new CommentReport (_encoding.GetString (record.Skip(2).ToArray()));
		}
		#endregion
	}
}

