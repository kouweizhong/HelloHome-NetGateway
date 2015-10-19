using System;

namespace NetHhGateway.Configuration.AppSettings
{
	public class AppSettingsBasedConfiguration : IConfigurationProvider
	{
		ISerialConfigurationProvider _serialConfigurationProvider;

		public AppSettingsBasedConfiguration (ISerialConfigurationProvider serialConfigurationProvider)
		{
			this._serialConfigurationProvider = serialConfigurationProvider;
			
		}

		#region IConfigurationProvider implementation
		public ISerialConfigurationProvider Serial 
		{
			get 
			{
				return _serialConfigurationProvider;
			}
		}
		#endregion
	}
}

