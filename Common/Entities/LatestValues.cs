using System;

namespace HelloHome.Common.Entities
{
    public class LatestValues
    {
		public virtual int NodeId { get; set; }
		public virtual float? VIn { get; set; }
        public virtual int SendErrorCount { get; set; }
        public virtual float? Temperature { get; set; }
        public virtual float? Humidity { get; set; }
        public virtual float? AtmosphericPressure { get; set; }
		public virtual int Rssi { get; set; }
		public virtual DateTime StartupTime { get; set; }
        internal virtual float MaxUpTimeRaw { get; set; }

        public virtual TimeSpan MaxUpTime
        {
            get
            {
                return TimeSpan.FromDays(MaxUpTimeRaw);
            }
            set
            {
                MaxUpTimeRaw = (float)value.TotalDays;
            }
        }
    }
}