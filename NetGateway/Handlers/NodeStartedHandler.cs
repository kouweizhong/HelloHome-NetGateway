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
		readonly ITouchNode _touchNode;
		readonly ITimeProvider _timeProvider;

		public NodeStartedHandler (
			IHelloHomeDbContext dbCtx,
			IFindNodeQuery findNodeQuery,
			ICreateNodeCommand createNodeCommand,
			ITouchNode touchNode,
			ITimeProvider timeProvider)
			: base (dbCtx)
		{
			this._timeProvider = timeProvider;
			_findNodeQuery = findNodeQuery;
			_createNodeCommand = createNodeCommand;
			_touchNode = touchNode;
		}

		protected override void Handle (NodeStartedReport request, IList<OutgoingMessage> outgoingMessages)
		{
			var node = _findNodeQuery.BySignature (request.Signature, NodeInclude.Facts);
			if (node == default (Node)) {
				node = _createNodeCommand.Execute (request.Signature, request.NeedNewRfAddress ? (byte)0 : request.FromNodeId);
				if (node.RfAddress != request.FromNodeId)
					outgoingMessages.Add (new NodeConfigCommand {
						signature = request.Signature,
						NewRfAddress = node.RfAddress
					});
			}
			node.Configuration.Version = $"{request.Major}.{request.Minor}";
			node.LatestValues.StartupTime = _timeProvider.UtcNow;
			_touchNode.Touch (node, request.Rssi);

		}
	}
}