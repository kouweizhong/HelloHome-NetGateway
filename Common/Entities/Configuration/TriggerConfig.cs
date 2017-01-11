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

            HasMany(x => x.Sensors)
                .WithMany(x => x.Triggers)
                .Map(x => x.ToTable("TriggerPort").MapLeftKey("triggerId").MapRightKey("portId"));

            Map<CronTrigger>(x => x.Requires("type").HasValue("C").HasColumnType("CHAR").HasMaxLength(1));
            Map<PushTrigger>(x => x.Requires("type").HasValue("P"));
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

    public class PulseTriggerConfig : EntityTypeConfiguration<PushTrigger>
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