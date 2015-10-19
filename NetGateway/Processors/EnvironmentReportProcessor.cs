﻿using System;
using NetHhGateway.Agents.NodeGateway.Domain;
using NetHhGateway.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using NetHhGateway.Agents.EmonCms;

namespace NetHhGateway.Processors
{
	public class EnvironmentReportProcessor : MessageProcessor<EnvironmentalReport>
	{
		IEmonCmsAgent emonCmsAgent;

		public EnvironmentReportProcessor (IEmonCmsAgent emonCmsAgent)
		{
			this.emonCmsAgent = emonCmsAgent;

		}

		public override System.Collections.Generic.IList<OutgoingMessage> ProcessInternal (EnvironmentalReport message)
		{
			using (var dbContext = new HelloHomeDbContext ()) {
				var node = dbContext.Nodes.Include (_ => _.LatestValues).Single (_ => _.RfAddress == message.FromNodeId);
				node.EnvironmentData = new List<EnvironmentData> { 
					new EnvironmentData {
						Timestamp = DateTime.Now,
						Temperature = message.Temperature > 0 ? message.Temperature : (float?)null,
						Humidity = message.Humidity > 0 ? message.Humidity : (float?)null,
						Pressure = message.Pressure > 0 ? message.Pressure : (int?)null
					}
				};
				if(message.Temperature > 0) node.LatestValues.Temperature = message.Temperature;
				if(message.Humidity > 0) node.LatestValues.Humidity = message.Humidity;
				if(message.Pressure > 0) node.LatestValues.Pressure = message.Pressure;
				dbContext.SaveChanges ();
				emonCmsAgent.Send (String.Format ("{{{0}_Temp:{1},{0}_Hum:{2}}}", node.RfAddress, message.Temperature, message.Humidity));

			}
			return null;
		}
	}
}

