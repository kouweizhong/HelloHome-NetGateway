using System;
using System.CodeDom;
using HelloHome.Common.Entities;

namespace HelloHome.NetGateway.Commands
{
    public interface ITouchNode : ICommand
    {
        void Touch(Node node, int rssi);
    }

    public class TouchNode : ITouchNode
    {
        private readonly ITimeProvider _timeProvider;

        public TouchNode(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public void Touch(Node node, int rssi)
        {
            if(node.NodeFacts == default(NodeFacts))
                throw new ArgumentException("node should be loaded with its facts for Touch to work");
            node.LastSeen = _timeProvider.UtcNow;
            node.LastRssi = rssi;
            node.NodeFacts.MaxUpTime = Math.Max(
                node.NodeFacts.MaxUpTime,
                (float) (_timeProvider.UtcNow - node.NodeFacts.LastStartupTime).TotalDays);
        }
    }
}