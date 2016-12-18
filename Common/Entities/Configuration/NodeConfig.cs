using System.Data.Entity.ModelConfiguration;

namespace HelloHome.Common.Entities.Configuration
{
	public class NodeConfig : EntityTypeConfiguration<Node>
	{
		public NodeConfig ()
		{
			ToTable ("Node");
			HasKey (_ => _.Id);
			Property (_ => _.Id)
				.HasColumnName ("nodeId")
				.IsRequired ();
			Property (_ => _.Signature)
				.HasColumnName ("signature")
				.IsRequired ();
			Property (_ => _.RfAddress)
				.HasColumnName ("RfAddress")
				.IsRequired ();

			HasOptional (_ => _.LatestValues).WithRequired ();
			HasOptional (_ => _.Configuration).WithRequired ();
			HasMany (_ => _.Ports).WithRequired ().HasForeignKey (_ => _.NodeId);
			HasMany (_ => _.Logs).WithRequired ().HasForeignKey (_ => _.NodeId);
			HasMany (_ => _.NodeHealthHistory).WithRequired ().HasForeignKey (x => x.NodeId);
			HasMany (_ => _.EnvironmentData).WithRequired ().HasForeignKey (_ => _.NodeId);
		}
	}
}

