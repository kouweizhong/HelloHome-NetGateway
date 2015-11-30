using System;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain
{
	public interface ISignedMessage
	{
		long Signature { get; }
		int OldSignature { get; }
	}
}

