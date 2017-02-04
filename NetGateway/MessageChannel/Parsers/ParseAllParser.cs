using HelloHome.NetGateway.MessageChannel.Domain.Base;
using HelloHome.NetGateway.MessageChannel.Domain.Reports;

namespace HelloHome.NetGateway.MessageChannel.Parsers
{
	public class ParseAllParser : IMessageParser
	{
		#region IMessageParser implementation
		public bool CanParse (byte[] record)
		{
			return true;
		}
		public Report Parse (byte[] record)
		{
			return new RawReport (record);
		}
		#endregion
	}
}

