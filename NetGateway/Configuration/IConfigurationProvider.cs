using System;

namespace NetHhGateway.Configuration
{
	public interface IConfigurationProvider
	{
		ISerialConfigurationProvider Serial { get; }

		IEmonCmsConfiguration EmonCms { get; }
	}
}

