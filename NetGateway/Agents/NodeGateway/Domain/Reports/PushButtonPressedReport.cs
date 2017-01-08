using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain.Reports
{
    public class PushButtonPressedReport : Report
    {
        public byte PushSensorNumber { get; set; }
    }
}