using System;

namespace HelloHome.NetGateway.Configuration
{
	public interface ISerialConfigurationProvider
	{
		string Port { get; }
	}
}

