using System;
using System.Data.Entity;
using HelloHome.Common.Entities;
using System.Linq;
using HelloHome.NetGateway.Agents.EmonCms;
using System.Collections.Generic;

namespace HelloHome.NetGateway
{
    public interface IEMonCmsUpdater
    {
        void Update();
    }

    public class EMonCmsUpdater : IEMonCmsUpdater
    {
        readonly IEmonCmsAgent _emonCmsAgent;

        public EMonCmsUpdater(IEmonCmsAgent emonCmsAgent)
        {
            _emonCmsAgent = emonCmsAgent;

        }

        public void Update()
        {
            using (var dbContext = new HelloHomeDbContext())
            {
                var nodes = dbContext.Nodes
                    .Include(_ => _.Ports)
                    .Include(_ => _.LatestValues)
                    .Where(_ => _.Configuration.EmonCmsNodeId.HasValue);
                foreach (var node in nodes)
                {
                    var values = new List<float>
                    {
                        node.LatestValues.VIn ?? 0,
                        node.LatestValues.Temperature ?? 0,
                        node.LatestValues.Humidity ?? 0,
                        node.LastSeen.HasValue ? (float) (DateTime.Now - node.LastSeen.Value).TotalDays : 0
                    };
                    foreach (var port in node.Ports.OfType<PulsePort>() .OrderBy(_ => _.Number))
                    {
                        values.Add(port.PulseCount);
                    }
                    values.Add(node.LatestValues.Rssi);
                    _emonCmsAgent.Send(node.Configuration.EmonCmsNodeId.Value, values);
                }
            }
        }
    }
}

