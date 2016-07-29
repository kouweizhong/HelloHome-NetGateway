using System.Collections.Generic;
using Common.Exceptions;
using HelloHome.Common.Entities;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.Logic;
using HelloHome.NetGateway.Queries;
using NLog;

namespace HelloHome.NetGateway.Handlers
{
    public class NodeInfoHandler : MessageHandler<NodeInfoReport>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IFindNodeQuery _findNodeQuery;
        private readonly ITouchNode _touchNode;

        public NodeInfoHandler(IFindNodeQuery findNodeQuery, ITouchNode touchNode)
        {
            _findNodeQuery = findNodeQuery;
            _touchNode = touchNode;
        }

        protected override void Handle(NodeInfoReport request, IList<OutgoingMessage> outgoingMessages)
        {
            var node = _findNodeQuery.ByRfId(request.FromNodeId);
            if (node == null)
                throw new NodeNotFoundException(request.FromNodeId);
            _touchNode.Touch(node, request.Rssi);

            node.LatestValues.SendErrorCount = request.SendErrorCount;
            var nodeInfo = new NodeInfoData { SendErrorCount = request.SendErrorCount };
            node.LatestValues.VIn = request.Voltage;
            nodeInfo.VIn = request.Voltage;
            node.NodeInfoData.Add(nodeInfo);
        }
    }
}