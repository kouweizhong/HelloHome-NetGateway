using System;
using NetHhGateway.Agents.NodeGateway.Domain;
using NetHhGateway.Entities;
using System.Collections.Generic;
using System.Linq;

namespace NetHhGateway.Processors
{
	public class UpdateUpTimeProcessor : IMessageProcessor
	{

		#region IMessageProcessor implementation

		public bool CanProcess (IncomingMessage message)
		{
			return true;
		}

		public IList<OutgoingMessage> Process (IncomingMessage message)
		{
			using (var _dbContext = new HelloHomeDbContext ()) {
				var node = _dbContext.Nodes.FirstOrDefault (_ => _.RfAddress == message.FromNodeId);
				if (node != null) {
					node.UpTime = (float)(DateTime.Now - (node.LastStartupTime ?? DateTime.Now)).TotalSeconds;
					node.MaxUpTime = Math.Max (node.UpTime, node.MaxUpTime);
					_dbContext.SaveChanges ();
				}
			}
			return null;
		}

		#endregion
	}
}

