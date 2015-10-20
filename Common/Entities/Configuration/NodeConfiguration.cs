using System;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelloHome.Common.Entities.Configuration
{
	public class NodeConfiguration : EntityTypeConfiguration<Node>
	{
		public NodeConfiguration ()
		{
			ToTable ("Node");
			HasKey (_ => _.NodeId);

			Property (_ => _.NodeId).HasColumnName ("nodeId").IsRequired ().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
			Property (_ => _.RfAddress).HasColumnName ("RfAddress");
			Property (_ => _.EmonCmsNodeId).HasColumnName ("emonCmsNodeId");
			Property (_ => _.ValidConfig).HasColumnName ("validConfig");
			Property (_ => _.Name).HasColumnName ("name").HasColumnType ("VARCHAR").HasMaxLength (50);
			Property (_ => _.LastStartupTime).HasColumnName ("LastStartupTime");
			Property (_ => _.Version).HasColumnName ("version");
			Property (_ => _.UpTime).HasColumnName ("upTime");
			Property (_ => _.MaxUpTime).HasColumnName ("maxUpTime");

			HasRequired (_ => _.Configuration).WithRequiredPrincipal();
			HasRequired (_ => _.LatestValues).WithRequiredPrincipal ();
			HasMany (_ => _.SubNodes).WithRequired(_ => _.Node).HasForeignKey(_ => _.NodeId);
			HasMany (_ => _.NodeInfoData).WithRequired ().HasForeignKey (_ => _.NodeId);
			HasMany (_ => _.EnvironmentData).WithRequired ().HasForeignKey (_ => _.NodeId);
		}
	}
}

