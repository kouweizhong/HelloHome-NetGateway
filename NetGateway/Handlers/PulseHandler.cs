using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HelloHome.Common.Entities;
using HelloHome.Common.Exceptions;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.Commands;
using HelloHome.NetGateway.MessageChannel.Domain.Base;
using HelloHome.NetGateway.MessageChannel.Domain.Reports;
using HelloHome.NetGateway.Queries;

namespace HelloHome.NetGateway.Handlers
{
	public class PulseHandler : MessageHandler<PulseReport>
	{
		readonly IFindNodeQuery _findNodeQuery;
		readonly ITouchNode _touchNode;

		public PulseHandler (IHelloHomeDbContext dbCtx, IFindNodeQuery findNodeQuery, ITouchNode touchNode) : base(dbCtx)
		{
			_touchNode = touchNode;
			_findNodeQuery = findNodeQuery;
		}

	    protected override async Task HandleAsync(PulseReport request, IList<OutgoingMessage> outgoingMessages, CancellationToken cToken)
	    {
	        var node = await _findNodeQuery.ByRfIdAsync (request.FromNodeId);
	        if(node == null)
	            throw new NodeNotFoundException(request.FromNodeId);
	        await _touchNode.TouchAsync (node, request.Rssi);
	    }
	}
}

