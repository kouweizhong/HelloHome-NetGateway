using System;
using System.Data.Entity.ModelConfiguration;
using HelloHome.Common.Entities;

namespace HelloHome.Common
{
	public class NodeFactConfiguration : EntityTypeConfiguration<NodeFacts>
	{
		public NodeFactConfiguration ()
		{
			ToTable ("NodeFacts");
			HasKey (x => x.NodeId);
			Property (x => x.LastStartupTime)
				.HasColumnName ("LastStartup")
				.IsRequired();
			Property (x => x.MaxUpTime)
				.HasColumnName ("MaxUpTime")
				.IsRequired();
			Property (x => x.Version)
				.HasColumnName ("Version")
				.IsRequired();
		}
	}
}

