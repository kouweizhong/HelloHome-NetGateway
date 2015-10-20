using System;

namespace HelloHome.NetGateway.Agents.NodeGateway.Parsers
{
	public class EnvironmentReportParser : IMessageParser
	{
		#region IMessageParser implementation
		public bool CanParse (byte[] record)
		{
			return record[1] == 0 + 2 << 2;
		}
		public Domain.Report Parse (byte[] record)
		{
			return new Domain.EnvironmentalReport 
			{
				FromNodeId = record[0],
				Temperature = ((float)BitConverter.ToInt16(record, 2))/100.0f,
				Humidity = (float)BitConverter.ToInt16(record, 4),
				Pressure = (int)BitConverter.ToInt16(record, 6),
			};
		}

		#endregion
	}
}

