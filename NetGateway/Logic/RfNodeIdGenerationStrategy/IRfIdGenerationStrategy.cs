using System;
using System.Collections.Generic;

namespace HelloHome.NetGateway.Commands.RfNodeIdGenerationStrategy
{
	public interface IRfIdGenerationStrategy
	{
		byte FindRfAddress (IList<byte> exisitingRfAddresses);
	}
}

