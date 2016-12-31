using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace HelloHome.Common.Entities
{

	public class NodeLog 
	{ 
		public virtual int Id { get; set; }
		public virtual int NodeId { get; set; }
		public virtual DateTime Time { get; set; }
		public virtual string Type { get; set; }
		public virtual string Data { get; set; }
	}
}
