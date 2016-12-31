using System;
using System.Collections.Generic;

namespace HelloHome.Common.Entities
{

	public class CommunicationHistory 
	{ 
		public virtual int Id { get; set; }
		public virtual int NodeId { get; set; }
	    public virtual string Type { get; set; }
		public virtual DateTime Timestamp { get; set; }
	    public virtual int Rssi { get; set; }
	}
}
