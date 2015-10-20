using System;

namespace HelloHome.Common.Entities
{
	public class EnvironmentData
	{
		public virtual int Id { get; set; }
		public virtual int NodeId { get; set; }
		public virtual DateTime Timestamp { get; set; }
		public virtual float? Temperature { get; set; }
		public virtual float? Humidity { get; set; }
		public virtual int? Pressure { get; set; }
	}
}

