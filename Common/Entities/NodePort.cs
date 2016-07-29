using System;
using System.Collections.Generic;

namespace HelloHome.Common.Entities
{
	public class NodePort
	{
		public virtual  int SubNodeId { get; set; } 
		public virtual Node Node { get; set; }
		public virtual int NodeId { get; set; } 
		public virtual int Number { get; set; }  
		public virtual string Name { get; set; }
		public virtual int PulseCount { get; set; }
		public virtual IList<PulseData> PulseData { get; set; }
	}
}

