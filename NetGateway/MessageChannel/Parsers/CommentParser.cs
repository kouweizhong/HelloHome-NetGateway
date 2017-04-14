using System.Linq;
using System.Text;
using HelloHome.NetGateway.MessageChannel.Domain.Base;
using HelloHome.NetGateway.MessageChannel.Domain.Reports;

namespace HelloHome.NetGateway.MessageChannel.Parsers
{
	public class CommentParser : IMessageParser
	{
		private readonly Encoding _encoding = Encoding.ASCII;

		#region IMessageParser implementation

	    public Report Parse (byte[] record)
		{
			return new CommentReport (_encoding.GetString (record.Skip(2).ToArray()));
		}
		#endregion
	}
}

