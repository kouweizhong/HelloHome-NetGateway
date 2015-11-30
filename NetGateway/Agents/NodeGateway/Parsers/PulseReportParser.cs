using System;

namespace HelloHome.NetGateway.Agents.NodeGateway.Parsers
{
	public class PulseReportParser : IMessageParser
	{
		public PulseReportParser ()
		{
		}

		#region IReportParser implementation

		public bool CanParse (byte[] record)
		{
			return record [3] == 0 + 1 << 2;
		}

		public Domain.Report Parse (byte[] record)
		{
			return new Domain.PulseReport {
				FromNodeId = record [0],
				Rssi = (int)BitConverter.ToInt16(record,1),
				SubNode = record [4],
				NewPulses = record[5],
			};
		}

		#endregion
	}
}

