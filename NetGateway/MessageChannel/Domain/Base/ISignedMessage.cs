namespace HelloHome.NetGateway.Agents.NodeGateway.Domain.Base
{
	public interface ISignedMessage
	{
		long Signature { get; }
		int OldSignature { get; }
	}
}

