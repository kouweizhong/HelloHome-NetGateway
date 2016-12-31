using System;
using System.Reflection;

namespace HelloHome.Common.Entities
{
	public class PulseHistory : CommunicationHistory
	{
	    public PulseHistory()
	    {
	        Type = "P";
	    }
		public virtual PulsePort Port { get; set; }
	    public virtual int PortId { get; set; }
		public virtual int NewPulses { get; set; }
		public virtual int Total { get; set; }
		public virtual bool IsOffset { get; set; }
	}
}

