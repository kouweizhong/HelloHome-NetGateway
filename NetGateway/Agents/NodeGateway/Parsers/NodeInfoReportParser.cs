using System;

namespace NetHhGateway.Agents.NodeGateway.Parsers
{
	public class NodeInfoReportParser : IMessageParser
	{
		#region IMessageParser implementation
		public bool CanParse (byte[] record)
		{
			return record [1] == 0 + 0 << 2;
		}
		public Domain.Report Parse (byte[] record)
		{
			var voltage = ((float)BitConverter.ToInt16 (record, 6)) / 100.0f;
			return new Domain.NodeInfoReport {
				FromNodeId = record [0],
				SendErrorCount = BitConverter.ToInt16(record, 2),
				StartCount = BitConverter.ToInt16(record, 4),
				Voltage = voltage > 0 ? voltage : (float?)null,
			};
		}
		#endregion
	}
}

