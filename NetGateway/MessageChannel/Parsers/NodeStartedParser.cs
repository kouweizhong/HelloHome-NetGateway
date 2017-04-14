using System;
using HelloHome.NetGateway.MessageChannel.Domain.Base;
using HelloHome.NetGateway.MessageChannel.Domain.Reports;
using HelloHome.NetGateway.MessageChannel.Parsers.Factory;

namespace HelloHome.NetGateway.MessageChannel.Parsers
{
    [ParserFor(0 + (3 << 2))]
	public class NodeStartedParser : IMessageParser
	{
		#region IMessageParser implementation

	    public Report Parse (byte[] record)
		{
            return new NodeStartedReport {
                FromNodeId = record [0],
                Rssi = (int)BitConverter.ToInt16 (record, 1),
                Major = record [4],
                Minor = record [5],
                Signature = BitConverter.ToInt64 (record, 6),
                NeedNewRfAddress = BitConverter.ToBoolean (record, 14),
            };
		}

		#endregion
	}
}

