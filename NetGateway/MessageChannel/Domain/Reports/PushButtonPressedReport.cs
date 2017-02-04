using HelloHome.NetGateway.MessageChannel.Domain.Base;

namespace HelloHome.NetGateway.MessageChannel.Domain.Reports
{
    public class PushButtonPressedReport : Report
    {
        public byte PushSensorNumber { get; set; }
    }
}