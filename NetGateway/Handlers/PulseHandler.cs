using System;
using System.Collections.Generic;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.Logic;
using HelloHome.NetGateway.Queries;

namespace HelloHome.NetGateway.Handlers
{
	public class PulseHandler : MessageHandler<PulseReport>
	{
		readonly IFindNodeQuery _findNodeQuery;
		readonly ITouchNode _touchNode;

		public PulseHandler (IFindNodeQuery findNodeQuery, ITouchNode touchNode)
		{
			this._touchNode = touchNode;
			this._findNodeQuery = findNodeQuery;
		}

		protected override void Handle (PulseReport request, IList<OutgoingMessage> outgoingMessages)
		{
			var node = _findNodeQuery.ByRfId (request.FromNodeId);
			_touchNode.Touch (node, request.Rssi);
		}
	}
}

