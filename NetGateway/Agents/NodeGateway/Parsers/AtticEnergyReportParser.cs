using System;

namespace NetHhGateway.Agents.NodeGateway.Parsers
{
	public class AtticEnergyReportParser : IReportParser
	{
		#region IReportParser implementation
		public bool CanParse (byte[] record)
		{
			throw new NotImplementedException ();
		}

		public Domain.Report Parse (byte[] record)
		{
			throw new NotImplementedException ();
		}
		#endregion
	}
}

