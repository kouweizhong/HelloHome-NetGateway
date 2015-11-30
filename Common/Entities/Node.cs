using System;
using System.Collections.Generic;

namespace HelloHome.Common.Entities
{
	public class Node
	{
		public Node ()
		{
		}

		public virtual int NodeId { get; set; }
		public virtual long Signature { get; set; }
		public virtual bool ValidConfig { get; set; }
		public virtual string Name { get; set; }
		public virtual int RfAddress { get; set; }
		public virtual int? EmonCmsNodeId { get; set; }
		public virtual DateTime? LastStartupTime { get; set; }
		public virtual DateTime? LastMessageReceivedTime { get; set; }
		public virtual float UpTime { get; set; }
		public virtual float MaxUpTime { get; set; }
		public virtual int LastRssi { get; set; }
		public virtual string Version { get; set; }

		public virtual NodeConfiguration Configuration { get; set; }
		public virtual NodeLatestValues LatestValues { get; set; }
		public virtual IList<SubNode> SubNodes { get; set; }
		public virtual IList<NodeInfoData> NodeInfoData { get; set; }
		public virtual IList<EnvironmentData> EnvironmentData { get; set; }
	}
}

