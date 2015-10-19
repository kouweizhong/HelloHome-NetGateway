using System;
using System.Collections.Generic;

namespace NetHhGateway.Logic.RfNodeIdGenerationStrategy
{
	public interface IRfNodeIdGenerationStrategy
	{
		byte FindRfAddress (IList<byte> exisitingRfAddresses);
	}
}

