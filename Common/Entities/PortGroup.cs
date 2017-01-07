using System.Collections.Generic;

namespace HelloHome.Common.Entities
{
    public abstract class PortGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SwitchSensorGroup : PortGroup
    {
        public List<SwitchSensor> Ports { get; set; }
    }

    public class PushSensorGroup : PortGroup
    {
        public List<PushSensor> Ports { get; set; }
    }

    public class RelayActuatorGroup : PortGroup
    {
        public List<RelayActuator> Ports { get; set; }
    }
}