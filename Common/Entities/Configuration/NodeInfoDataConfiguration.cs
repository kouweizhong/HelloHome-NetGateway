using System;
using System.Data.Entity.ModelConfiguration;

namespace HelloHome.Common.Entities.Configuration
{
	public class NodeInfoDataConfiguration : EntityTypeConfiguration<NodeInfoData>
	{
		public NodeInfoDataConfiguration ()
		{
			ToTable ("NodeInfoData");
			HasKey (_ => _.Id);
			Property (_ => _.Id).HasColumnName ("id");
			Property (_ => _.NodeId).HasColumnName ("nodeId");
			Property (_ => _.Timestamp).HasColumnName ("timestamp");
			Property (_ => _.VIn).HasColumnName ("Vin");
			Property (_ => _.SendErrorCount).HasColumnName ("sendErrorCount");
		}
	}
}

