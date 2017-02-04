using System;
using HelloHome.NetGateway.MessageChannel.Domain.Base;
using HelloHome.NetGateway.MessageChannel.Domain.Reports;
using HelloHome.NetGateway.MessageChannel.Parsers.Factory;

namespace HelloHome.NetGateway.MessageChannel.Parsers
{
    [ParserFor(0 + (4 << 2))]
    public class PushButtonPressedReportParser : IMessageParser
    {
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