namespace HelloHome.NetGateway.MessageChannel.Domain.Base
{
	public interface ISignedMessage
	{
		long Signature { get; }
		int OldSignature { get; }
	}
}

