
using HelloHome.NetGateway.MessageChannel.Domain.Base;

namespace HelloHome.NetGateway.MessageChannel.Parsers
{
	public interface IMessageParser
	{
		bool CanParse (byte[] record);
		Report Parse(byte[] record);
	}
}

