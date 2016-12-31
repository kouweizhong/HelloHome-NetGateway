using System.Data.Entity.ModelConfiguration;

namespace HelloHome.Common.Entities.Configuration
{
    public class NodeLogConfig : EntityTypeConfiguration<NodeLog>
    {
        public NodeLogConfig()
        {
            ToTable("Log");
            HasKey(_ => _.Id);

            Property(_ => _.Id).HasColumnName("logId");
            Property(_ => _.NodeId).HasColumnName("nodeId");
            Property(_ => _.Time).HasColumnName("time");
            Property(_ => _.Type).HasColumnName("type");
            Property(_ => _.Data).HasColumnName("data");
        }
    }
}