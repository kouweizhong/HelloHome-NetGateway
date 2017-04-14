using System.Configuration;

namespace HelloHome.NetGateway.Configuration.AppSettings
{
	public class AppSettingsBasedConfiguration : IConfigurationProvider, ISerialConfigurationProvider, IEmonCmsConfiguration
	{
		#region IConfigurationProvider implementation

		public ISerialConfigurationProvider Serial  { get { return this; } }
		public IEmonCmsConfiguration EmonCms { get { return this; } }

		#endregion

		#region ISerialConfigurationProvider implementation

		public string Port {
			get 
			{
				var appSetting = ConfigurationManager.AppSettings ["Serial:port"];
				if (appSetting == null)
					throw new ConfigurationErrorsException ("Serial.port key not found in appSettings. Please check configuration");
				return appSetting.ToString();
			}
		}

		#endregion

		#region IEmonCmsConfiguration implementation

		public string ApiKey {
			get 
			{
				var appSetting = ConfigurationManager.AppSettings ["EmonCms:ApiKey"];
				if (appSetting == null)
					throw new ConfigurationErrorsException ("EmonCms:ApiKey key not found in appSettings. Please check configuration");
				return appSetting.ToString();
			}
		}

		#endregion
	}
}

