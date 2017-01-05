using System;
using System.CodeDom;
using System.Threading.Tasks;
using HelloHome.Common;
using HelloHome.Common.Entities;
using NLog;

namespace HelloHome.NetGateway.Commands
{
    public interface ITouchNode : ICommand
    {
        Task TouchAsync(Node node, int rssi);
    }

    public class TouchNode : ITouchNode
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ();

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
            Logger.Debug("Node with signature {0} was touched", node.Signature);
            await Task.Yield();
        }
    }
}