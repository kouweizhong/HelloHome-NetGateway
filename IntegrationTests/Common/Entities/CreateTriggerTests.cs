using System.Collections.Generic;
using HelloHome.Common.Entities;
using Xunit;

namespace IntegrationTests.Common.Entities
{
    public class CreateTriggerTests : EntityTest
    {
        private readonly List<SensorPort> _ports;

        private const long BaseSignature = 76587486538549;
        private const byte TestNetwork = 66;

        public CreateTriggerTests()
        {
            _ports = new List<SensorPort>
            {
                new PushSensor
                {
                    Number = 1,
                    Node = new Node { Signature = BaseSignature +1,  }
                }
            };
        }

        [Fact]
        public void create_pulsetrigger()
        {
            var trigger  = new PushTrigger
            {
                Sensors = _ports
            };
            DbCtx.Triggers.Add(trigger);
            DbCtx.SaveChanges();
        }

        [Fact]
        public void create_switchTrigger()
        {
            var trigger  = new SwitchTrigger
            {
                Sensors = _ports,
                TriggerOnState = true,
            };
            DbCtx.Triggers.Add(trigger);
            DbCtx.SaveChanges();
        }

        [Fact]
        public void create_varioTrigger()
        {
            var trigger  = new VarioTrigger
            {
                Sensors = _ports,
            };
            DbCtx.Triggers.Add(trigger);
            DbCtx.SaveChanges();
        }

        [Fact]
        public void create_cronTrigger()
        {
            var trigger  = new CronTrigger
            {
                Sensors = _ports,
                CronExpression = "* * * 8"
            };
            DbCtx.Triggers.Add(trigger);
            DbCtx.SaveChanges();
        }
    }
}