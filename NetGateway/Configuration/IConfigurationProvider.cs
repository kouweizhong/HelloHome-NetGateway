namespace HelloHome.NetGateway.Configuration
{
	public interface IConfigurationProvider
	{
		ISerialConfigurationProvider Serial { get; }

		IEmonCmsConfiguration EmonCms { get; }
	}
}

