using System;
using HelloHome.NetGateway.MessageChannel.Domain.Base;
using HelloHome.NetGateway.MessageChannel.Domain.Reports;

namespace HelloHome.NetGateway.MessageChannel.Parsers
{
    public class PushButtonPressedReportParser : IMessageParser
    {
        public bool CanParse(byte[] record)
        {
            return record [3] == 0 + 4 << 2;
        }

        public Report Parse(byte[] record)
        {
            return new PushButtonPressedReport {
                FromNodeId = record [0],
                Rssi = BitConverter.ToInt16(record,1),
                PushSensorNumber = record[4]
            };
        }
    }
}