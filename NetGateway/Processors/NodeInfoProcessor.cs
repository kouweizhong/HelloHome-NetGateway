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
		HelloHomeDbContext _dbContext;

		public NodeInfoProcessor (HelloHomeDbContext helloHomeDbContext)
		{
			this._dbContext = helloHomeDbContext;			
		}

		public override IList<OutgoingMessage> ProcessInternal (NodeInfoReport message)
		{
			_dbContext.Entry (Node).Reference (_ => _.LatestValues).Load ();
			Node.LatestValues.SendErrorCount = message.SendErrorCount;
			Node.LatestValues.VIn = message.Voltage;
			if (message.Voltage.HasValue || message.SendErrorCount > 0) {
				Node.NodeInfoData = new List<NodeInfoData> { 
					new NodeInfoData { Timestamp = DateTime.Now, SendErrorCount = message.SendErrorCount, VIn = message.Voltage }
				};
			}
			return null;
		}
	}
}

