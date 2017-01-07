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
			Map<PulseSensor> (m => m.Requires ("type").HasValue ("P").HasColumnType("CHAR").HasMaxLength(1));
		    Map<PushSensor> (m => m.Requires ("type").HasValue ("H"));
		    Map<SwitchSensor> (m => m.Requires ("type").HasValue ("S"));
		    Map<VarioSensor> (m => m.Requires ("type").HasValue ("V"));
			Map<RelayActuator> (m => m.Requires ("type").HasValue ("R"));
		}
	}

	public class PulseSensorConfig : EntityTypeConfiguration<PulseSensor>
	{ 
		public PulseSensorConfig ()
		{
			Property (_ => _.PulseCount).HasColumnName ("pulseCount");			
		}
	}

    public class SwithSensorConfig : EntityTypeConfiguration<SwitchSensor>
    {
        public SwithSensorConfig()
        {
            Property(_ => _.State).HasColumnName("state");
        }
    }

    public class VarioSensorConfig : EntityTypeConfiguration<VarioSensor>
    {
        public VarioSensorConfig()
        {
            Property(_ => _.Value).HasColumnName("value");
        }
    }

    public class RelayActuatorConfig : EntityTypeConfiguration<RelayActuator>
    {
        public RelayActuatorConfig()
        {
        }
    }
}
