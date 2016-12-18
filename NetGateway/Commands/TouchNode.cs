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
            if(node.LatestValues == default(LatestValues))
                throw new ArgumentException("node should be loaded with its latestValues for Touch to work");
            node.LastSeen = _timeProvider.UtcNow;
            node.LatestValues.Rssi = rssi;
            node.LatestValues.MaxUpTime = Math.Max(
                node.LatestValues.MaxUpTime,
                (float) (_timeProvider.UtcNow - node.LatestValues.StartupTime).TotalDays);
		}
    }
}