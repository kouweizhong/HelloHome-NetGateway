using System;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.Common.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using HelloHome.NetGateway.Agents.EmonCms;
using log4net;

namespace HelloHome.NetGateway.Processors
{
	public class EnvironmentReportProcessor : MessageProcessor<EnvironmentalReport>
	{
		readonly static ILog log = LogManager.GetLogger(typeof(EnvironmentReportProcessor).Name);
		readonly HelloHomeDbContext _dbContext;

		public EnvironmentReportProcessor (HelloHomeDbContext dbContext)
		{
			this._dbContext = dbContext;			
			log.Debug ($"Injected with DbContext with hash {dbContext.ContextId}");
		}

		public override IList<OutgoingMessage> ProcessInternal (EnvironmentalReport message)
		{
			Node.EnvironmentData = new List<EnvironmentData> { 
				new EnvironmentData {
					Timestamp = DateTime.Now,
					Temperature = message.Temperature > 0 ? message.Temperature : (float?)null,
					Humidity = message.Humidity > 0 ? message.Humidity : (float?)null,
					Pressure = message.Pressure > 0 ? message.Pressure : (int?)null
				}
			};
			_dbContext.Entry (Node).Reference (_ => _.LatestValues).Load ();
			if (message.Temperature > 0)
				Node.LatestValues.Temperature = message.Temperature;
			if (message.Humidity > 0)
				Node.LatestValues.Humidity = message.Humidity;
			if (message.Pressure > 0)
				Node.LatestValues.Pressure = message.Pressure;
			return null;
		}
	}
}

