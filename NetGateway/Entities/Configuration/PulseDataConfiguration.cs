using System;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetHhGateway.Entities.Configuration
{
	public class PulseDataConfiguration : EntityTypeConfiguration<PulseData>
	{
		public PulseDataConfiguration ()
		{
			ToTable ("PulseData");
			HasKey (_ => _.PulseDataId);
			Property (_ => _.PulseDataId).HasColumnName ("id").IsRequired ().HasDatabaseGeneratedOption (DatabaseGeneratedOption.Identity);	
			Property (_ => _.Timestamp).HasColumnName ("timestamp");
			Property (_ => _.NewPulses).HasColumnName ("newPulses");
			Property (_ => _.NewValue).HasColumnName ("newValue");
		}
	}
}

