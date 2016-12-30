using System;
using System.Data.Entity.ModelConfiguration;

namespace HelloHome.Common.Entities.Configuration
{
	public class LatestValuesConfiguration : EntityTypeConfiguration<LatestValues>
	{
		public LatestValuesConfiguration ()
		{
			ToTable ("NodeLatestValues");
			HasKey (x => x.NodeId);
			Property (_ => _.Temperature).HasColumnName ("Temperature");
			Property (_ => _.Humidity).HasColumnName ("Humidity");
			Property (_ => _.AtmosphericPressure).HasColumnName ("Pressure");
			Property (_ => _.VIn).HasColumnName ("VIn");
		    Property (_ => _.SendErrorCount).HasColumnName ("SendErrorCount");
		    Property (_ => _.MaxUpTimeRaw).HasColumnName ("maxUpTime");

		    Ignore(_ => _.MaxUpTime);
		}
	}
}

