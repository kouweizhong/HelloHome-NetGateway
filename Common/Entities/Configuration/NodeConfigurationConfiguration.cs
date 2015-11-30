using System;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelloHome.Common.Entities.Configuration
{
	public class NodeConfigurationConfiguration : EntityTypeConfiguration<Entities.NodeConfiguration>
	{
		public NodeConfigurationConfiguration ()
		{
			ToTable ("Node");
			HasKey (_ => _.NodeId);
			Property (_ => _.NodeId).HasColumnName ("nodeId").IsRequired ().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);;
			Property (_ => _.Hal1Pin).HasColumnName ("confHal1");
			Property (_ => _.Hal2Pin).HasColumnName ("confHal2");
		}
	}
}

