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
			Property (_ => _.Temperature).HasColumnName ("currentTemperature");
			Property (_ => _.Humidity).HasColumnName ("currentHumidity");
			Property (_ => _.AtmosphericPressure).HasColumnName ("currentPressure");
			Property (_ => _.VIn).HasColumnName ("currentVIn");
			Property (_ => _.SendErrorCount).HasColumnName ("currentSendErrorCount");
		}
	}
}

