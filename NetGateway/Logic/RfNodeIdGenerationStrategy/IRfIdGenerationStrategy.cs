using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HelloHome.NetGateway.Logic.RfNodeIdGenerationStrategy
{
	public interface IRfIdGenerationStrategy
	{
		byte FindRfAddress (IList<byte> exisitingRfAddresses);
	    Task<byte> FindAvailableRfAddressAsync(byte network, CancellationToken cToken, byte suggestion = 0);
	}
}

