using System;
using System.Text;

namespace HelloHome.NetGateway.Agents.NodeGateway.Domain
{
	public class RawReport : Report
	{

		public byte[] Bytes { get; set; }

		public RawReport (byte[] rawMessage)
		{
			Bytes = rawMessage;			
		}

		public override string ToString ()
		{
			var sb = new StringBuilder (Bytes.Length * 2);
			foreach (var b in Bytes)
				sb.Append(b.ToString("X2"));
					return string.Format ("[RawMessage: Bytes={0}]", sb);
		}
	}
}

