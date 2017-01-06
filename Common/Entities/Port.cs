using System;
using System.Collections.Generic;

namespace HelloHome.Common.Entities
{
	public abstract class Port
	{
		public virtual int NodePortId { get; set; }
		public virtual Node Node { get; set; }
		public virtual int NodeId { get; set; }
		public virtual int Number { get; set; }  
		public virtual string Name { get; set; }
	}

	public abstract class SensorPort : Port
	{ 
	}

	public abstract class ActuatorPort : Port
	{ 
	}

	public class PulsePort : SensorPort 
	{ 
		public virtual int PulseCount { get; set; }
		public virtual IList<PulseHistory> PulseData { get; set; }
	}

	public class SwitchPort : SensorPort 
	{ 
		public virtual bool State { get; set; }
	}

	public class VarioPort : SensorPort 
	{
		public virtual int Value { get; set; }
	}

	public class RelayPort : ActuatorPort 
	{ 
	}
}

