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
		public virtual byte RfId { get; set; }
		public virtual DateTime? LastSeen { get; set; }
		public virtual int LastRssi { get; set; }

		public virtual NodeConfiguration Configuration { get; set; }
		public virtual LatestValues LatestValues { get; set; }
		public virtual NodeFacts NodeFacts { get; set; }
		public virtual IList<NodePort> Ports { get; set; }
		public virtual IList<NodeInfoData> NodeInfoData { get; set; }
		public virtual IList<EnvironmentData> EnvironmentData { get; set; }
	}
}

