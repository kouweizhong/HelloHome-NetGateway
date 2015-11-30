using System;
using System.Configuration;

namespace HelloHome.NetGateway.Pipeline.Configuration
{
	public class PipelineConfigurationSection : ConfigurationSection
	{
		[ConfigurationProperty("", IsRequired=true, IsDefaultCollection=true)]
		public PipelineModuleCollection Modules { 
			get{ return (PipelineModuleCollection)this [""]; } 
			set{ this [""] = value; }
		}
	}
}

