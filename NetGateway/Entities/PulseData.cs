using System;

namespace NetHhGateway.Entities
{
	public class PulseData
	{
		public virtual int PulseDataId { get; set; }
		public virtual DateTime Timestamp { get; set; }
		public virtual int NewPulses { get; set; }
		public virtual int NewValue { get; set; }
	}
}

