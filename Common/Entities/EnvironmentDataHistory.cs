using System;

namespace HelloHome.Common.Entities
{
	public class EnvironmentDataHistory : CommunicationHistory
	{
		public virtual float? Temperature { get; set; }
		public virtual float? Humidity { get; set; }
		public virtual int? Pressure { get; set; }
	}
}

