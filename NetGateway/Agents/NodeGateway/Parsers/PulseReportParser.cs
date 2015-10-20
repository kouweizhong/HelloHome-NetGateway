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
			return record [1] == 0 + 1 << 2;
		}

		public Domain.Report Parse (byte[] record)
		{
			return new Domain.PulseReport {
				FromNodeId = record [0],
				SubNode = record [2],
				NewPulses = record[3],
			};
		}

		#endregion
	}
}

