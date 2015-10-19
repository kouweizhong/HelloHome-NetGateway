
namespace NetHhGateway.Agents.NodeGateway.Encoders
{
	
	public interface IMessageEncoder
	{
		bool CanEncode (Domain.Message message);
		byte[] Encode(Domain.Message message);
	}
}
