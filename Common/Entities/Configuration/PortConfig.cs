using System;
using System.Data.Entity.ModelConfiguration;

namespace HelloHome.Common.Entities.Configuration
{
	public class PortConfig : EntityTypeConfiguration<Port>
	{
		public PortConfig ()
		{
			ToTable ("Port");
			HasKey (_ => _.NodePortId);
			Property (_ => _.NodePortId).HasColumnName ("portId").IsRequired ();
			Property (_ => _.NodeId).HasColumnName ("nodeId").IsRequired ();
			Property (_ => _.Number).HasColumnName ("number").IsRequired ();
			Property (_ => _.Name).HasColumnName ("name");
		    HasRequired(_ => _.Node).WithMany(_ => _.Ports).HasForeignKey(_ => _.NodeId);
			Map<PulsePort> (m => m.Requires ("type").HasValue ("P").HasColumnType("CHAR").HasMaxLength(1));
			Map<SwitchPort> (m => m.Requires ("type").HasValue ("S"));
			Map<VarioPort> (m => m.Requires ("type").HasValue ("V"));
			Map<RelayPort> (m => m.Requires ("type").HasValue ("R"));
		}
	}

	public class PulseSensorConfig : EntityTypeConfiguration<PulsePort>
	{ 
		public PulseSensorConfig ()
		{
			Property (_ => _.PulseCount).HasColumnName ("pulseCount");			
		}
	}

    public class SwithSensorConfig : EntityTypeConfiguration<SwitchPort>
    {
        public SwithSensorConfig()
        {
            Property(_ => _.State).HasColumnName("state");
        }
    }

    public class VarioSensorConfig : EntityTypeConfiguration<VarioPort>
    {
        public VarioSensorConfig()
        {
            Property(_ => _.Value).HasColumnName("value");
        }
    }

    public class RelayActuatorConfig : EntityTypeConfiguration<RelayPort>
    {
        public RelayActuatorConfig()
        {
        }
    }
}
