using System;

namespace HelloHome.Common.Entities
{
	public class NodeInfoData
	{
		public virtual int Id { get; set; }
		public virtual Node Node { get; set; }
		public virtual DateTime Timestamp { get; set; }
		public virtual float? VIn { get; set; }
		public virtual int SendErrorCount { get; set; }
	}
}

