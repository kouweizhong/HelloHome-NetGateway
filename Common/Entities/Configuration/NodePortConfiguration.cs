using System;
using System.Data.Entity.ModelConfiguration;

namespace HelloHome.Common.Entities.Configuration
{
	public class NodePortConfiguration : EntityTypeConfiguration<NodePort>
	{
		public NodePortConfiguration ()
		{
			ToTable ("NodePort");
			HasKey (_ => _.NodePortId);
			Property (_ => _.NodePortId).HasColumnName ("NodePortId").IsRequired ();
			Property (_ => _.NodeId).HasColumnName ("nodeId").IsRequired ();
			Property (_ => _.Number).HasColumnName ("number").IsRequired ();
			Property (_ => _.Name).HasColumnName ("name");
			Property (_ => _.PulseCount).HasColumnName ("pulseCount");
		}
	}
}
