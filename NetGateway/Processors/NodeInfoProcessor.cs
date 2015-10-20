using System;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.Common.Entities;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using HelloHome.NetGateway.Agents.EmonCms;

namespace HelloHome.NetGateway.Processors
{
	public class NodeInfoProcessor : MessageProcessor<NodeInfoReport>
	{
		public override System.Collections.Generic.IList<OutgoingMessage> ProcessInternal (NodeInfoReport message)
		{
			using (var dbContext = new HelloHomeDbContext ()) {
				var node = dbContext.Nodes.Include (_ => _.LatestValues).Single (_ => _.RfAddress == message.FromNodeId);
				node.LatestValues.SendErrorCount = message.SendErrorCount;
				node.LatestValues.VIn = message.Voltage;
				if (message.Voltage.HasValue || message.SendErrorCount > 0) {
					node.NodeInfoData = new List<NodeInfoData> { 
						new NodeInfoData { Timestamp = DateTime.Now, SendErrorCount = message.SendErrorCount, VIn = message.Voltage }
					};
				}
				dbContext.SaveChanges ();
			}
			return null;
		}
	}
}

