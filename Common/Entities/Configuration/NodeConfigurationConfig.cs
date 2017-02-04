using System.Data.Entity.ModelConfiguration;

namespace HelloHome.Common.Entities.Configuration
{
	public class NodeConfigurationConfig : EntityTypeConfiguration<Entities.NodeConfiguration>
	{
		public NodeConfigurationConfig ()
		{
			ToTable ("NodeConfig");
			HasKey (x => x.NodeId);
			Property (x => x.NodeId)
				.HasColumnName ("nodeId")
				.IsRequired ();
			Property (x => x.Name)
				.HasColumnName ("name")
				.IsOptional ();
		    Property (x => x.EmonCmsNodeId)
		        .HasColumnName ("emonCmsNodeId")
		        .IsOptional();
		    Property (x => x.Version)
		        .HasColumnName ("version")
		        .IsOptional();
		}
	}
}

