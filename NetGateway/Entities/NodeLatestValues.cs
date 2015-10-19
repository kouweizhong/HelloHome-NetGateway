using System;

namespace NetHhGateway.Entities
{
	public class NodeLatestValues
	{
		public virtual int NodeId { get; set; }
		public virtual float? Temperature { get; set; }
		public virtual float? Humidity { get; set; }
		public virtual int? Pressure { get; set; }
		public virtual float? VIn { get; set; }
		public virtual int SendErrorCount { get; set; }
	}
}

