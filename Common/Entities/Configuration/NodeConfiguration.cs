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
			Property (_ => _.Signature)
				.HasColumnName ("signature")
				.IsRequired ();
			Property (_ => _.RfId)
				.HasColumnName ("RfId")
				.IsRequired();
			Property (x => x.LastRssi)
				.HasColumnName ("LastRssi")
				.IsRequired ();

			HasOptional (x => x.LatestValues).WithRequired ();
			HasOptional (_ => _.Configuration).WithRequired ();
			HasOptional (x => x.NodeFacts).WithRequired();
			HasMany (_ => _.NodeInfoData).WithRequired ().HasForeignKey(x=>x.NodeId);
			HasMany (_ => _.EnvironmentData).WithRequired ().HasForeignKey (_ => _.NodeId);
		}
	}
}

