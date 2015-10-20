using System;
using System.Data.Entity;
using NetHhGateway.Entities;
using System.Linq;
using NetHhGateway.Agents.EmonCms;
using System.Collections.Generic;

namespace NetHhGateway
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
					foreach (var subNode in node.SubNodes) {
						values.Add (subNode.PulseCount);
					}
					_emonCmsAgent.Send (node.EmonCmsNodeId.Value, values);		
				}
			}
		}
	}
}

