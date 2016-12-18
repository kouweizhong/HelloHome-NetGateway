using System;

namespace HelloHome.Common.Entities
{
	public class NodeHealthHistory : CommunicationHistory
	{
		public virtual float? VIn { get; set; }
		public virtual int SendErrorCount { get; set; }
	}
}

