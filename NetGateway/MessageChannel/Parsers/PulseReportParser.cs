using System;
using HelloHome.NetGateway.MessageChannel.Domain.Base;
using HelloHome.NetGateway.MessageChannel.Domain.Reports;
using HelloHome.NetGateway.MessageChannel.Parsers.Factory;

namespace HelloHome.NetGateway.MessageChannel.Parsers
{
    [ParserFor(0 + (1 << 2))]
    public class PulseReportParser : IMessageParser
	{
		public PulseReportParser ()
		{
		}

		#region IReportParser implementation

	    public Report Parse (byte[] record)
		{
			return new PulseReport {
				FromNodeId = record [0],
				Rssi = (int)BitConverter.ToInt16(record,1),
				SubNode = record [4],
				NewPulses = record[5],
			};
		}

		#endregion
	}
}

