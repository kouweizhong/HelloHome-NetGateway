using System.ComponentModel.DataAnnotations.Schema;
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
				.IsRequired ()
			    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			Property (_ => _.Signature)
				.HasColumnName ("signature")
				.IsRequired ();
		    Property (_ => _.RfAddress)
		        .HasColumnName ("rfAddress")
		        .IsRequired ();
		    Property (_ => _.RfNetwork)
		        .HasColumnName ("rfNetwork")
		        .IsRequired ();
		    Property (_ => _.LastSeen)
		        .HasColumnName ("lastSeen");

		    HasOptional (_ => _.LatestValues).WithRequired ();
			HasOptional (_ => _.Configuration).WithRequired ();
			HasMany (_ => _.Ports).WithRequired ().HasForeignKey (_ => _.NodeId);
			HasMany (_ => _.Logs).WithRequired ().HasForeignKey (_ => _.NodeId);
		    HasMany(_ => _.CommunicationHistory).WithRequired().HasForeignKey(_ => _.NodeId);
		}
	}
}

