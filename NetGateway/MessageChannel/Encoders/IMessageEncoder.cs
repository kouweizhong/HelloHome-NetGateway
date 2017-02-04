
using HelloHome.NetGateway.MessageChannel.Domain.Base;

namespace HelloHome.NetGateway.MessageChannel.Encoders
{
	
	public interface IMessageEncoder
	{
		bool CanEncode (Message message);
		byte[] Encode(Message message);
	}
}
