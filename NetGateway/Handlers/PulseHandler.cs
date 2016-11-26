using System;
using System.Collections.Generic;
using HelloHome.Common.Entities;
using HelloHome.Common.Exceptions;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.Commands;
using HelloHome.NetGateway.Queries;

namespace HelloHome.NetGateway.Handlers
{
	public class PulseHandler : MessageHandler<PulseReport>
	{
		readonly IFindNodeQuery _findNodeQuery;
		readonly ITouchNode _touchNode;

		public PulseHandler (IHelloHomeDbContext dbCtx, IFindNodeQuery findNodeQuery, ITouchNode touchNode) : base(dbCtx)
		{
			this._touchNode = touchNode;
			this._findNodeQuery = findNodeQuery;
		}

		protected override void Handle (PulseReport request, IList<OutgoingMessage> outgoingMessages)
		{
			var node = _findNodeQuery.ByRfId (request.FromNodeId);
            if(node == null)
                throw new NodeNotFoundException(request.FromNodeId);
			_touchNode.Touch (node, request.Rssi);
		}
	}
}

