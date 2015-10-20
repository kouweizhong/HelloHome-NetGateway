
namespace HelloHome.NetGateway.Agents.NodeGateway.Parsers
{
	public class ParseAllParser : IMessageParser
	{
		#region IMessageParser implementation
		public bool CanParse (byte[] record)
		{
			return true;
		}
		public Domain.Report Parse (byte[] record)
		{
			return new Domain.RawReport (record);
		}
		#endregion
	}
}

