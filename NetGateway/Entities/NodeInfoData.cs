using System;

namespace NetHhGateway.Entities
{
	public class NodeInfoData
	{
		public virtual int Id { get; set; }
		public virtual int NodeId { get; set; }
		public virtual DateTime Timestamp { get; set; }
		public virtual float? VIn { get; set; }
		public virtual int SendErrorCount { get; set; }
	}
}

