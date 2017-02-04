
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;

namespace HelloHome.NetGateway.Agents.NodeGateway.Parsers
{
	public interface IMessageParser
	{
		bool CanParse (byte[] record);
		Report Parse(byte[] record);
	}
}

