using System;
using System.Collections.Generic;

namespace HelloHome.Common.Entities
{
	public class Node
	{
		public Node ()
		{
		}

		public virtual int Id { get; set; }
		public virtual long Signature { get; set; }
		public virtual int RfId { get; set; }
		public virtual DateTime? LastSeen { get; set; }

		public virtual NodeConfiguration Configuration { get; set; }
		public virtual IList<NodeInfoData> NodeInfoData { get; set; }
		public virtual IList<EnvironmentData> EnvironmentData { get; set; }
	}
}

