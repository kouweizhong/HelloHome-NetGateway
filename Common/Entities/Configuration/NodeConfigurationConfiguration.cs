using System;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelloHome.Common.Entities.Configuration
{
	public class NodeConfigurationConfiguration : EntityTypeConfiguration<Entities.NodeConfiguration>
	{
		public NodeConfigurationConfiguration ()
		{
			ToTable ("NodeConfig");
			HasKey (x => x.NodeId);
			Property (x => x.NodeId)
				.HasColumnName ("nodeId")
				.IsRequired ();
			Property (x => x.Name)
				.HasColumnName ("Name")
				.IsOptional ();
			Property (x => x.EmonCmsNodeId)
				.HasColumnName ("EmonCmsNodeId")
				.IsOptional();
		}
	}
}

