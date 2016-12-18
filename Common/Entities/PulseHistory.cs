using System;

namespace HelloHome.Common.Entities
{
	public class PulseHistory : CommunicationHistory
	{
		public virtual int PulsePortId { get; set; }
		public virtual int NewPulses { get; set; }
		public virtual int NewValue { get; set; }
		public virtual bool IsOffset { get; set; }
	}
}

