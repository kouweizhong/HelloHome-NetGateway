using System;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.Common.Entities;
using System.Linq;
using HelloHome.NetGateway.Logic.RfNodeIdGenerationStrategy;
using System.Collections.Generic;

namespace HelloHome.NetGateway.Processors
{
	public class NodeStartedProcessor : MessageProcessor<NodeStartedReport>
	{
		readonly IRfNodeIdGenerationStrategy _rfNodeIdGenerationStrategy;
		readonly HelloHomeDbContext _dbContext;

		public NodeStartedProcessor (HelloHomeDbContext dbContext, IRfNodeIdGenerationStrategy rfNodeIdGenerationStrategy)
		{
			this._dbContext = dbContext;
			this._rfNodeIdGenerationStrategy = rfNodeIdGenerationStrategy;			
		}

		public override IList<OutgoingMessage> ProcessInternal (NodeStartedReport message)
		{
			Node.Version = String.Format ("{0}.{1}", message.Major, message.Minor);
			Node.LastStartupTime = DateTime.Now;

			if (message.NeedNewRfAddress) {
				//Find an available nodeId
				var usedIds = _dbContext.Nodes.Select (_ => (byte)_.RfId).Distinct ().ToList ();
				var newRfAddress = _rfNodeIdGenerationStrategy.FindRfAddress (usedIds);
				return new List<OutgoingMessage> { new NodeConfigCommand { NewRfAddress = newRfAddress } };
			}
			return null;
		}
	}
}

