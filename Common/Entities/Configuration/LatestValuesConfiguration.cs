using System.Data.Entity.ModelConfiguration;

namespace HelloHome.Common.Entities.Configuration
{
	public class LatestValuesConfiguration : EntityTypeConfiguration<LatestValues>
	{
		public LatestValuesConfiguration ()
		{
			ToTable ("NodeLatestValues");
			HasKey (x => x.NodeId);
		    Property (_ => _.NodeId).HasColumnName ("nodeId");
		    Property (_ => _.Temperature).HasColumnName ("temperature");
		    Property (_ => _.Humidity).HasColumnName ("humidity");
			Property (_ => _.AtmosphericPressure).HasColumnName ("pressure");
			Property (_ => _.VIn).HasColumnName ("vIn");
		    Property (_ => _.SendErrorCount).HasColumnName ("sendErrorCount");
		    Property (_ => _.MaxUpTimeRaw).HasColumnName ("maxUpTime");
		    Property (_ => _.Rssi).HasColumnName ("rssi");
		    Property (_ => _.StartupTime).HasColumnName ("startupTime");

		    Ignore(_ => _.MaxUpTime);
		}
	}
}

