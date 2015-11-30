using System;
using System.Data.Entity;
using HelloHome.Common.Entities;
using System.Linq;
using HelloHome.NetGateway.Agents.EmonCms;
using System.Collections.Generic;

namespace HelloHome.NetGateway
{
	public class EmonCmsUpdater
	{
		IEmonCmsAgent _emonCmsAgent;

		public EmonCmsUpdater (IEmonCmsAgent emonCmsAgent)
		{
			_emonCmsAgent = emonCmsAgent;
			
		}

		public void Update ()
		{
			using (var dbContext = new HelloHomeDbContext ()) {
				var nodes = dbContext.Nodes
					.Include (_ => _.SubNodes)
					.Include (_ => _.LatestValues)
					.Where (_ => _.EmonCmsNodeId != null);
				foreach (var node in nodes) {
					var values = new List<float> ();
					values.Add (node.LatestValues.VIn ?? 0);
					values.Add (node.LatestValues.Temperature ?? 0);
					values.Add (node.LatestValues.Humidity ?? 0);
					values.Add (node.UpTime);
					foreach (var subNode in node.SubNodes.OrderBy(_ => _.Number)) {
						values.Add (subNode.PulseCount);
					}
					values.Add (node.LastRssi);
					_emonCmsAgent.Send (node.EmonCmsNodeId.Value, values);		
				}
			}
		}
	}
}

