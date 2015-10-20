using System;
using System.Collections.Generic;

namespace HelloHome.NetGateway.Logic.RfNodeIdGenerationStrategy
{
	public interface IRfNodeIdGenerationStrategy
	{
		byte FindRfAddress (IList<byte> exisitingRfAddresses);
	}
}

