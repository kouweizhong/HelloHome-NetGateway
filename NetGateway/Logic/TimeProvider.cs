using System;

namespace HelloHome.NetGateway.Logic
{
    public interface ITimeProvider
    {
        DateTime UtcNow { get; }
    }

    public class TimeProvider : ITimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}