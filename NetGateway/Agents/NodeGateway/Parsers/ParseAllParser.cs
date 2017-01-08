
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Reports;

namespace HelloHome.NetGateway.Agents.NodeGateway.Parsers
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

