namespace HelloHome.Common.Entities
{
	public class NodeConfiguration
	{
		public virtual int NodeId { get; set; }
		public virtual string Name { get; set; }
		public virtual int? EmonCmsNodeId { get; set; }
		public virtual string Version { get; set; }

	}
}

