using System;
using System.Runtime.Remoting;

namespace HelloHome.Common.Entities
{
	public class NodeHealthHistory : CommunicationHistory
	{
	    public NodeHealthHistory()
	    {
	        Type = "H";
	    }
		public virtual float? VIn { get; set; }
		public virtual int SendErrorCount { get; set; }
	}
}

