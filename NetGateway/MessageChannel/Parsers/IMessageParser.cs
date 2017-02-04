
using HelloHome.NetGateway.MessageChannel.Domain.Base;

namespace HelloHome.NetGateway.MessageChannel.Parsers
{
	public interface IMessageParser
	{
	    Report Parse(byte[] record);
	}
}

