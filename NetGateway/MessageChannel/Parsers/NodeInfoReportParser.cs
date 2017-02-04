using System;
using HelloHome.NetGateway.MessageChannel.Domain.Base;
using HelloHome.NetGateway.MessageChannel.Domain.Reports;

namespace HelloHome.NetGateway.MessageChannel.Parsers
{
	public class NodeInfoReportParser : IMessageParser
	{
		#region IMessageParser implementation
		public bool CanParse (byte[] record)
		{
			return record [3] == 0 + 0 << 2;
		}
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

