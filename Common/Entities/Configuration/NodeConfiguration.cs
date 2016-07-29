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
			HasKey (_ => _.Id);
			Property (_ => _.Signature).HasColumnName ("signature");
			Property (_ => _.RfId).HasColumnName ("RfId");

			HasRequired (_ => _.Configuration).WithRequiredPrincipal ();
			HasMany (_ => _.NodeInfoData).WithRequired ().HasForeignKey (_ => _.Id);
			HasMany (_ => _.EnvironmentData).WithRequired ().HasForeignKey (_ => _.NodeId);
		}
	}
}

