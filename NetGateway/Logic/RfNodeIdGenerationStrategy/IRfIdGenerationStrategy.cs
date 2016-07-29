using System;
using System.Collections.Generic;

namespace HelloHome.NetGateway.Logic.RfNodeIdGenerationStrategy
{
	public interface IRfIdGenerationStrategy
	{
		byte FindRfAddress (IList<byte> exisitingRfAddresses);
	}
}

