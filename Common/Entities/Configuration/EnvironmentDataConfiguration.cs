using System;
using System.Data.Entity.ModelConfiguration;
using HelloHome.Common.Entities;

namespace HelloHome.Common.Entities.Configuration
{
	public class EnvironmentDataConfiguration : EntityTypeConfiguration<EnvironmentDataHistory>
	{
		public EnvironmentDataConfiguration ()
		{
			ToTable ("EnvironmentData");
			HasKey (_ => _.Id);
			Property (_ => _.Id).HasColumnName ("id");
			Property (_ => _.NodeId).HasColumnName ("nodeId");
			Property (_ => _.Timestamp).HasColumnName ("timestamp");
			Property (_ => _.Temperature).HasColumnName ("temperature");
			Property (_ => _.Humidity).HasColumnName ("humidity");
			Property (_ => _.Pressure).HasColumnName ("pressure");
		}
	}
}

