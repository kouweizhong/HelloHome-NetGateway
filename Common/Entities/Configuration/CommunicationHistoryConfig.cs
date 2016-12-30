﻿using System.Data.Entity.ModelConfiguration;

namespace HelloHome.Common.Entities.Configuration
{
    public class CommunicationHistoryConfig : EntityTypeConfiguration<CommunicationHistory>
    {
        public CommunicationHistoryConfig()
        {
            ToTable("CommunicationHistory");
            HasKey(_ => _.Id);

            Property(_ => _.Timestamp).HasColumnName("time");
        }
    }

    public class EnvironmentDataHistoryConfiguration : EntityTypeConfiguration<EnvironmentDataHistory>
    {
        public EnvironmentDataHistoryConfiguration ()
        {
            ToTable ("EnvironmentDataHistory");
            Property (_ => _.Temperature).HasColumnName ("temperature");
            Property (_ => _.Humidity).HasColumnName ("humidity");
            Property (_ => _.Pressure).HasColumnName ("pressure");
        }
    }

    public class NodeHealthHisotryConfig : EntityTypeConfiguration<NodeHealthHistory>
    {
        public NodeHealthHisotryConfig()
        {
            ToTable("NodeHealthHistory");
            Property(_ => _.SendErrorCount).HasColumnName("sendErrorCount");
            Property(_ => _.VIn).HasColumnName("vIn");
        }
    }

    public class PulseDataHistoryConfig : EntityTypeConfiguration<PulseHistory>
    {
        public PulseDataHistoryConfig()
        {
            ToTable("PulseHistory");
            Property(_ => _.PortId).HasColumnName("portId");
            Property(_ => _.NewPulses).HasColumnName("newPulses");
            Property(_ => _.Total).HasColumnName("total");
            Property(_ => _.IsOffset).HasColumnName("isOffset");
            HasRequired(_ => _.Port).WithMany().HasForeignKey(_ => _.PortId);
        }
    }
}