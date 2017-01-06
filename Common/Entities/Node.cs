using System;
using System.Collections.Generic;

namespace HelloHome.Common.Entities
{
	public class Node
	{
		public virtual int Id { get; set; }
		public virtual long Signature { get; set; }
	    public virtual byte RfAddress { get; set; }
	    public virtual byte RfNetwork { get; set; }
	    public virtual DateTime? LastSeen { get; set; }

	    public virtual NodeConfiguration Configuration { get; set; }
		public virtual LatestValues LatestValues { get; set; }
		public virtual IList<NodeLog> Logs { get; set; }
		public virtual IList<Port> Ports { get; set; }
		public virtual IList<CommunicationHistory> CommunicationHistory { get; set; }

	    public void AddLog(string type, string data = null)
	    {
	        if(Logs == null)
	            Logs = new List<NodeLog>();
	        Logs.Add(new NodeLog { Time = TimeProvider.Current.UtcNow, NodeId = this.Id, Type = type, Data = data });
	    }
	}

	
}

