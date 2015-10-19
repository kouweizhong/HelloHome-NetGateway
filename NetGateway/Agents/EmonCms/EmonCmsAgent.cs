using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Common.Extentions;
using System.Linq;
using NetHhGateway.Configuration;
using System.Net;

namespace NetHhGateway.Agents.EmonCms
{
	public class EmonCmsAgent
	{
		private const string urlBase = "http://emoncms.org/input/post.json?";

		IEmonCmsConfiguration config;

		public EmonCmsAgent (IEmonCmsConfiguration config)
		{
			this.config = config;
			
		}

		public void Send<T>(int? nodeId, T data, DateTime? timestamp) {
			var p = new List<String> { "json=" + JsonConvert.SerializeObject (data) };
			if (nodeId.HasValue)
				p.Add ("node=" + nodeId.Value);
			if (timestamp.HasValue)
				p.Add ("time="+timestamp.Value.ToEpoch());
			Send (p.ToArray ());
		}

		private void Send(params string[] parameters) {
			var paramList = parameters.ToList ();
			paramList.Add ("apikey="+config.ApiKey);
			var url = urlBase + String.Join ("&", paramList);
			var request = WebRequest.CreateHttp(url);
			var response = request.GetResponse() as HttpWebResponse;
		}
	}
}

