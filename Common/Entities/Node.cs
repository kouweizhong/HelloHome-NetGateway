using System;
using System.Collections.Generic;

namespace HelloHome.Common.Entities
{
	public class Node
	{
		public virtual int Id { get; set; }
		public virtual long Signature { get; set; }
		public virtual byte RfAddress { get; set; }

		public virtual NodeConfiguration Configuration { get; set; }
		public virtual LatestValues LatestValues { get; set; }
		public virtual IList<NodeLog> Logs { get; set; }
		public virtual IList<NodePort> Ports { get; set; }
		public virtual IList<NodeHealthHistory> NodeHealthHistory { get; set; }
		public virtual IList<EnvironmentDataHistory> EnvironmentData { get; set; }
		public virtual DateTime? LastSeen { get; set; }
	}

	
}

