using System;
using System.Data.Entity.ModelConfiguration;

namespace HelloHome.Common.Entities.Configuration
{
	public class NodePortConfig : EntityTypeConfiguration<NodePort>
	{
		public NodePortConfig ()
		{
			ToTable ("Port");
			HasKey (_ => _.NodePortId);
			Property (_ => _.NodePortId).HasColumnName ("portId").IsRequired ();
			Property (_ => _.NodeId).HasColumnName ("nodeId").IsRequired ();
			Property (_ => _.Number).HasColumnName ("number").IsRequired ();
			Property (_ => _.Name).HasColumnName ("name");
			Map<PulsePort> (m => m.Requires ("type").HasValue ("P"));
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
