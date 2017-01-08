using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HelloHome.Common.Entities;
using HelloHome.Common.Exceptions;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Reports;
using HelloHome.NetGateway.Commands;
using HelloHome.NetGateway.Queries;
using NLog;

namespace HelloHome.NetGateway.Handlers
{
    public class NodeInfoHandler : MessageHandler<NodeInfoReport>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IFindNodeQuery _findNodeQuery;
        private readonly ITouchNode _touchNode;

		public NodeInfoHandler(IHelloHomeDbContext dbCtx, IFindNodeQuery findNodeQuery, ITouchNode touchNode) : base(dbCtx)
        {
            _findNodeQuery = findNodeQuery;
            _touchNode = touchNode;
        }

        protected override async Task HandleAsync(NodeInfoReport request, IList<OutgoingMessage> outgoingMessages, CancellationToken cToken)
        {
            var node = await _findNodeQuery.ByRfIdAsync(request.FromNodeId);
            if (node == null)
                throw new NodeNotFoundException(request.FromNodeId);
            await _touchNode.TouchAsync(node, request.Rssi);

            node.LatestValues.SendErrorCount = request.SendErrorCount;
            var nodeInfo = new NodeHealthHistory { SendErrorCount = request.SendErrorCount };
            node.LatestValues.VIn = request.Voltage;
            nodeInfo.VIn = request.Voltage;
            node.CommunicationHistory.Add(nodeInfo);
        }
    }
}