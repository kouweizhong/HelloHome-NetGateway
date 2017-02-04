
using HelloHome.NetGateway.MessageChannel.Domain.Base;

namespace HelloHome.NetGateway.MessageChannel.Encoders
{
	
	public interface IMessageEncoder
	{
	    byte[] Encode(Message message);
	}
}
