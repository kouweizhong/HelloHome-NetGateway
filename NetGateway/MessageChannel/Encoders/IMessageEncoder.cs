
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;

namespace HelloHome.NetGateway.Agents.NodeGateway.Encoders
{
	
	public interface IMessageEncoder
	{
		bool CanEncode (Message message);
		byte[] Encode(Message message);
	}
}
