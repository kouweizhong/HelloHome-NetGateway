using System.Text;
using System.Linq;

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
		public Domain.Report Parse (byte[] record)
		{
			return new Domain.CommentReport (_encoding.GetString (record.Skip(2).ToArray()));
		}
		#endregion
	}
}

