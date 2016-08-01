using System;
using System.Data.SqlTypes;

namespace HelloHome.Common.Entities
{
    public class NodeFacts
    {
        public virtual int NodeId { get; set; }
        public virtual string Version { get; set; }
        public virtual DateTime LastStartupTime { get; set; }
        public virtual float MaxUpTime { get; set; }
    }
}