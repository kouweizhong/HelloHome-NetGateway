using System;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Reports;

namespace HelloHome.NetGateway.Agents.NodeGateway.Parsers
{
	public class NodeStartedParser : IMessageParser
	{
		#region IMessageParser implementation

		public bool CanParse (byte[] record)
		{
			return record [3] == 0 + 3 << 2;
		}

		public Report Parse (byte[] record)
		{
			if (record.Length > 13)
				return new NodeStartedReport { 
					FromNodeId = record [0],
					Rssi = (int)BitConverter.ToInt16 (record, 1),
					Major = record [4],
					Minor = record [5],
					OldSignature = BitConverter.ToInt32 (record, 6),
					Signature = BitConverter.ToInt64 (record, 10),
					NeedNewRfAddress = BitConverter.ToBoolean (record, 18),
				};
			return new NodeStartedReport { 
				FromNodeId = record [0],
				Rssi = (int)BitConverter.ToInt16 (record, 1),
				Major = record [4],
				Minor = record [5],
				OldSignature = BitConverter.ToInt32 (record, 6),
				NeedNewRfAddress = BitConverter.ToBoolean (record, 10),
			};
		}

		#endregion
	}
}

