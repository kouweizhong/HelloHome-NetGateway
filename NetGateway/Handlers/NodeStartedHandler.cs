using System.Collections.Generic;
using HelloHome.Common.Entities;
using HelloHome.Common.Entities.Includes;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.Commands;
using HelloHome.NetGateway.Queries;
using NLog;

namespace HelloHome.NetGateway.Handlers
{
	public class NodeStartedHandler : MessageHandler<NodeStartedReport>
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger ();

		private readonly IFindNodeQuery _findNodeQuery;
		private readonly ICreateNodeCommand _createNodeCommand;
		private readonly ITouchNode _touchNode;

		public NodeStartedHandler (IHelloHomeDbContext dbCtx, IFindNodeQuery findNodeQuery, ICreateNodeCommand createNodeCommand, ITouchNode touchNode) : base (dbCtx)
		{
			_findNodeQuery = findNodeQuery;
			_createNodeCommand = createNodeCommand;
			_touchNode = touchNode;
		}

		protected override void Handle (NodeStartedReport request, IList<OutgoingMessage> outgoingMessages)
		{
			var node = _findNodeQuery.BySignature (request.Signature, NodeInclude.Facts);
			if (node == default (Node)) {
				node = _createNodeCommand.Execute (request.Signature, request.NeedNewRfAddress?(byte)0:request.FromNodeId);
				if(node.RfId != request.FromNodeId)
					outgoingMessages.Add (new NodeConfigCommand {
						signature = request.Signature,
						NewRfAddress = node.RfId
				});
			}
			node.NodeFacts.Version = $"{request.Major}.{request.Minor}";
			_touchNode.Touch (node, request.Rssi);
		}
	}
}