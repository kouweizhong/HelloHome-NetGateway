using System.Collections.Generic;

namespace HelloHome.Common.Entities
{
    public class PortGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Port> Ports { get; set; }
    }
}