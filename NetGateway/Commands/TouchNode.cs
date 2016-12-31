using System;
using System.CodeDom;
using System.Threading.Tasks;
using HelloHome.Common;
using HelloHome.Common.Entities;

namespace HelloHome.NetGateway.Commands
{
    public interface ITouchNode : ICommand
    {
        Task TouchAsync(Node node, int rssi);
    }

    public class TouchNode : ITouchNode
    {
        private readonly ITimeProvider _timeProvider;

        public TouchNode(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public async Task TouchAsync(Node node, int rssi)
        {
            if(node.LatestValues == default(LatestValues))
                throw new ArgumentException("node entity should be loaded with its latestValues for Touch to work");
            node.LastSeen = _timeProvider.UtcNow;
            node.LatestValues.Rssi = rssi;
            node.LatestValues.MaxUpTime =
                TimeSpan.FromDays(
                    Math.Max(
                        node.LatestValues.MaxUpTime.TotalDays,
                        (_timeProvider.UtcNow - node.LatestValues.StartupTime).TotalDays
                    )
                );
            await Task.Yield();
        }
    }
}