﻿using System.Collections.Generic;

namespace HelloHome.Common.Entities
{
    public abstract class Trigger
    {
        public int Id { get; set; }
    }

    public class CronTrigger : Trigger
    {
        public string CronExpression { get; set; }
    }

    public class DelayTrigger : Trigger
    {
        public int DelayMs { get; set; }
    }

    public abstract class NodePortBasedTrigger : Trigger
    {
        public virtual int NodePortGroupId { get; set; }
        public virtual PortGroup PortGroup { get; set; }
    }

    public class PulseTrigger : NodePortBasedTrigger
    {
    }

    public class SwitchTrigger : NodePortBasedTrigger
    {
        public bool? TriggerOnState { get; set; }
    }

    public class VarioTrigger : NodePortBasedTrigger
    {
    }
}