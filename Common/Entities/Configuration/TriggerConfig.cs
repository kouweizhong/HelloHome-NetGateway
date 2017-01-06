using System.CodeDom;
using System.Data.Entity.ModelConfiguration;

namespace HelloHome.Common.Entities.Configuration
{
    public class TriggerConfig : EntityTypeConfiguration<Trigger>
    {
        public TriggerConfig()
        {
            ToTable("Trigger");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("triggerId").IsRequired();

            Map<CronTrigger>(x => x.Requires("type").HasValue("C").HasColumnType("CHAR").HasMaxLength(1));
            Map<DelayTrigger>(x => x.Requires("type").HasValue("D"));
            Map<PulseTrigger>(x => x.Requires("type").HasValue("P"));
            Map<SwitchTrigger>(x => x.Requires("type").HasValue("S"));
            Map<VarioTrigger>(x => x.Requires("type").HasValue("V"));
        }
    }

    public class CronTriggerConfig : EntityTypeConfiguration<CronTrigger>
    {
        public CronTriggerConfig()
        {
            Property(x => x.CronExpression)
                .HasColumnName("cronExpression")
                .HasColumnType("VARCHAR")
                .HasMaxLength(100)
                .IsRequired();
        }
    }

    public class DelayTriggerConfig : EntityTypeConfiguration<DelayTrigger>
    {
        public DelayTriggerConfig()
        {
            Property(x => x.DelayMs).HasColumnName("delayMs").IsRequired();
        }
    }

    public class NodePortBasedTriggerConfig : EntityTypeConfiguration<NodePortBasedTrigger>
    {
        public NodePortBasedTriggerConfig()
        {
            Property(x => x.NodePortGroupId).HasColumnName("portGroupId").IsRequired();
            HasRequired(x => x.PortGroup).WithMany().HasForeignKey(x => x.NodePortGroupId);
        }
    }

    public class PulseTriggerConfig : EntityTypeConfiguration<PulseTrigger>
    {
        public PulseTriggerConfig()
        {

        }
    }

    public class SwitchTriggerConfig : EntityTypeConfiguration<SwitchTrigger>
    {
        public SwitchTriggerConfig()
        {
            Property(x => x.TriggerOnState).HasColumnName("triggerOnState").IsOptional();
        }
    }

    public class VarioTriggerConfig : EntityTypeConfiguration<VarioTrigger>
    {
        public VarioTriggerConfig()
        {

        }
    }
}