using System;
using System.Configuration;

namespace NetHhGateway.Configuration.AppSettings
{
	public class AppSettingsBasedSerialConfiguration : ISerialConfigurationProvider
	{
		#region ISerialConfigurationProvider implementation

		public string Port 
		{
			get 
			{
				var appSetting = ConfigurationManager.AppSettings ["Serial:port"];
				if (appSetting == null)
					throw new ConfigurationErrorsException ("Serial.port key not found in appSettings. Please check configuration");
				return appSetting.ToString();
			}
		}

		#endregion
	}
}

