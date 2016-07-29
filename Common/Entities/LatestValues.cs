namespace HelloHome.Common.Entities
{
    public class LatestValues
    {
        public virtual Node Node  { get; set; }
        public virtual float? VIn { get; set; }
        public virtual int SendErrorCount { get; set; }
        public virtual float? Temperature { get; set; }
        public virtual float? Humidity { get; set; }
        public virtual float? AtmosphericPressure { get; set; }
    }
}