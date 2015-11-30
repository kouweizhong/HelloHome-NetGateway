using System;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.Common.Entities;
using System.Linq;
using System.Collections.Generic;
using HelloHome.NetGateway.Agents.EmonCms;
using System.Data.Entity;

namespace HelloHome.NetGateway.Processors
{
	public class PulseReportProcessor : MessageProcessor<PulseReport>
	{
		HelloHomeDbContext _dbContext;

		public PulseReportProcessor (HelloHomeDbContext dbContext)
		{
			this._dbContext = dbContext;
			
		}

		public override IList<OutgoingMessage> ProcessInternal (PulseReport message)
		{
			var subNode = _dbContext.SubNodes.Include (_ => _.Node).FirstOrDefault (_ => _.Node.NodeId == Node.NodeId && _.Number == message.SubNode);
			if (subNode == null) {
				Node.SubNodes.Add (subNode = new SubNode { NodeId = Node.NodeId, Number = message.SubNode });
			}
			subNode.PulseCount += message.NewPulses;
			subNode.PulseData = new List<PulseData> { 
				new PulseData {
					Timestamp = DateTime.Now,
					NewPulses = message.NewPulses,
					NewValue = subNode.PulseCount
				}
			};

			return null;
		}
	}
}

