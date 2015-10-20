using System;
using System.Data.Entity.ModelConfiguration;

namespace HelloHome.Common.Entities.Configuration
{
	public class NodeLatestValuesConfiguration : EntityTypeConfiguration<NodeLatestValues>
	{
		public NodeLatestValuesConfiguration ()
		{
			ToTable ("Node");
			HasKey (_ => _.NodeId);
			Property (_ => _.NodeId).HasColumnName ("nodeId");
			Property (_ => _.Temperature).HasColumnName ("currentTemperature");
			Property (_ => _.Humidity).HasColumnName ("currentHumidity");
			Property (_ => _.Pressure).HasColumnName ("currentPressure");
			Property (_ => _.VIn).HasColumnName ("currentVIn");
			Property (_ => _.SendErrorCount).HasColumnName ("currentSendErrorCount");
		}
	}
}

