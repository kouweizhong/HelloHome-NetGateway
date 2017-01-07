using System.Data.Entity.ModelConfiguration;

namespace HelloHome.Common.Entities.Configuration
{
    public class PortGroupConfig : EntityTypeConfiguration<PortGroup>
    {
        public PortGroupConfig()
        {
            ToTable("PortGroup");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("portGroupId").IsRequired();
            Map<PushSensorGroup>(x => x.Requires("type").HasValue("H").HasColumnType("CHAR").HasMaxLength(1));
            Map<SwitchSensorGroup>(x => x.Requires("type").HasValue("S"));
            Map<RelayActuatorGroup>(x => x.Requires("type").HasValue("R"));
        }
    }

    public class PushSensorGroupConfig : EntityTypeConfiguration<PushSensorGroup>
    {
        public PushSensorGroupConfig()
        {
            HasMany(x => x.Ports)
                .WithMany()
                .Map(m => m.ToTable("PortGroupPort").MapRightKey("portId").MapLeftKey("portGroupId"));
        }
    }
    public class SwitchSensorGroupConfig : EntityTypeConfiguration<SwitchSensorGroup>
    {
        public SwitchSensorGroupConfig()
        {
            HasMany(x => x.Ports)
                .WithMany();
        }
    }
    public class RelayActuatorGroupConfig : EntityTypeConfiguration<RelayActuatorGroup>
    {
        public RelayActuatorGroupConfig()
        {
            HasMany(x => x.Ports)
                .WithMany();
        }
    }
}