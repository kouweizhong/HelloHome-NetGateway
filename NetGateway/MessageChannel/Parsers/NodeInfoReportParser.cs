using System;
using HelloHome.NetGateway.MessageChannel.Domain.Base;
using HelloHome.NetGateway.MessageChannel.Domain.Reports;
using HelloHome.NetGateway.MessageChannel.Parsers.Factory;

namespace HelloHome.NetGateway.MessageChannel.Parsers
{
    [ParserFor(0 + (0 << 2))]
    public class NodeInfoReportParser : IMessageParser
	{
		#region IMessageParser implementation

	    public Report Parse (byte[] record)
		{
			var voltage = ((float)BitConverter.ToInt16 (record, 8)) / 100.0f;
			return new NodeInfoReport {
				FromNodeId = record [0],
				Rssi = (int)BitConverter.ToInt16(record,1),
				SendErrorCount = BitConverter.ToInt16(record, 4),
				StartCount = BitConverter.ToInt16(record, 6),
				Voltage = voltage > 0 ? voltage : (float?)null,
			};
		}
		#endregion
	}
}

