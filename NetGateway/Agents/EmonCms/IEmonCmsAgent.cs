using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Common.Extentions;
using System.Linq;
using HelloHome.NetGateway.Configuration;
using System.Net;
using System.IO;

namespace HelloHome.NetGateway.Agents.EmonCms
{
	public interface IEmonCmsAgent {
		void Send<T> (T data);
		void Send<T> (int nodeId, IList<T> values, DateTime? timestamp = null) where T : struct;
		void Send(string json);
	}
	
}
