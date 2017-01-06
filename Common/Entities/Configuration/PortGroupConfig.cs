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

            HasMany(x => x.Ports)
                .WithMany()
                .Map(m => m.ToTable("PortGroupPort").MapRightKey("portId").MapLeftKey("portGroupId"));
        }
    }
}