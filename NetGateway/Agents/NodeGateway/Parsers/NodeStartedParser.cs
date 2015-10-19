using System;

namespace NetHhGateway.Agents.NodeGateway.Parsers
{
	public class NodeStartedParser : IMessageParser
	{
		#region IMessageParser implementation

		public bool CanParse (byte[] record)
		{
			return record [1] == 0 + 3 << 2;
		}

		public NetHhGateway.Agents.NodeGateway.Domain.Report Parse (byte[] record)
		{
			return new Domain.NodeStartedReport { 
				FromNodeId = record[0],
				Major = record[2],
				Minor = record[3],
				Signature = BitConverter.ToInt32(record, 4),
				NeedNewRfAddress = BitConverter.ToBoolean(record, 8),
			};
		}

		#endregion
	}
}

