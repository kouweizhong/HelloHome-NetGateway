using System;
using NetHhGateway.Agents.NodeGateway.Domain;
using NetHhGateway.Entities;
using System.Linq;
using System.Collections.Generic;
using NetHhGateway.Agents.EmonCms;
using System.Data.Entity;

namespace NetHhGateway.Processors
{
	public class PulseReportProcessor : MessageProcessor<PulseReport>
	{
		IEmonCmsAgent emonCmsAgent;

		public PulseReportProcessor (IEmonCmsAgent emonCmsAgent)
		{
			this.emonCmsAgent = emonCmsAgent;
		}

		public override IList<OutgoingMessage> ProcessInternal (PulseReport message)
		{
			using (var _dbContext = new HelloHomeDbContext ()) {
				var subNode = _dbContext.SubNodes.Include(_ => _.Node).FirstOrDefault (_ => _.Node.RfAddress == message.FromNodeId && _.Number == message.SubNode);
				if (subNode == null) {
					var node = _dbContext.Nodes.Single(_ => _.RfAddress == message.FromNodeId);
					_dbContext.SubNodes.Add (subNode = new SubNode { NodeId = node.NodeId, Number = message.SubNode });
				}
				subNode.PulseCount += message.NewPulses;
				subNode.PulseData = new List<PulseData> { new PulseData {
						Timestamp = DateTime.Now,
						NewPulses = message.NewPulses,
						NewValue = subNode.PulseCount
					} };
				_dbContext.SaveChanges ();
				emonCmsAgent.Send (String.Format ("{{{0}_{1}:{2}}}", subNode.Node.RfAddress, subNode.Number, subNode.PulseCount));
			}

			return null;
		}
	}
}

