using System;
using System.Configuration;

namespace HelloHome.NetGateway.Pipeline.Configuration
{
	public class PipelineModuleElement : ConfigurationElement
	{
		[ConfigurationProperty ("name", IsKey = true, IsRequired = true)]
		public string Name { 
			get { return (string)base ["name"]; } 
			set { base ["name"] = value; } 
		}

		[ConfigurationProperty ("type", IsRequired = true)]
		public string Type { 
			get { return (string)base ["type"]; } 
			set { base ["type"] = value; } 
		}
	}
}

