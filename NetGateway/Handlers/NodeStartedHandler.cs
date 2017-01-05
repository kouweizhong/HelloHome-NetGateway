using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HelloHome.Common;
using HelloHome.Common.Entities;
using HelloHome.Common.Entities.Includes;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.Commands;
using HelloHome.NetGateway.Logic.RfNodeIdGenerationStrategy;
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
	    private readonly IRfIdGenerationStrategy _rfIdGenerationStrategy;
	    readonly ITimeProvider _timeProvider;

		public NodeStartedHandler (
			IHelloHomeDbContext dbCtx,
			IFindNodeQuery findNodeQuery,
			ICreateNodeCommand createNodeCommand,
			ITouchNode touchNode,
			IRfIdGenerationStrategy rfIdGenerationStrategy,
			ITimeProvider timeProvider)
			: base (dbCtx)
		{
			_timeProvider = timeProvider;
			_findNodeQuery = findNodeQuery;
			_createNodeCommand = createNodeCommand;
			_touchNode = touchNode;
		    _rfIdGenerationStrategy = rfIdGenerationStrategy;
		}

		protected override async Task HandleAsync (NodeStartedReport request, IList<OutgoingMessage> outgoingMessages, CancellationToken cToken)
		{
			var node = await _findNodeQuery.BySignatureAsync (request.Signature, NodeInclude.Facts | NodeInclude.Config);
		    if (node == default (Node))
		    {
			    Logger.Debug("Node not found based on signature {0}. A node will be created.", request.Signature);
		        var rfId = await _rfIdGenerationStrategy.FindAvailableRfAddressAsync(
		            Constants.NetworkId,
		            cToken,
		            request.NeedNewRfAddress?(byte)0: request.FromNodeId);
				node = await _createNodeCommand.ExecuteAsync (request.Signature, rfId);
			} else if (request.NeedNewRfAddress)
			{
			    Logger.Debug("Node was found based on signature {0} but a new rfAddress was requestd", request.Signature);
			    node.RfAddress = await _rfIdGenerationStrategy.FindAvailableRfAddressAsync(Constants.NetworkId, cToken);
			}

		    if (node.RfAddress != request.FromNodeId)
		    {
		        Logger.Debug("Node received a new rfAddress ({0}). Therefore a configCommand should be sent.", node.RfAddress);
		        outgoingMessages.Add (new NodeConfigCommand {
		            signature = request.Signature,
		            NewRfAddress = node.RfAddress
		        });
		    }
		    node.Configuration.Version = $"{request.Major}.{request.Minor}";
			node.LatestValues.StartupTime = _timeProvider.UtcNow;
		    node.AddLog("STRT");
		    await _touchNode.TouchAsync (node, request.Rssi);
		}
	}
}