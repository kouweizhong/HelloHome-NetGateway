using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using HelloHome.Common.Entities;
using Xunit;

namespace IntegrationTests.Common.Entities
{
    public class CreateTriggerTests : IClassFixture<EntityTestFixture>
    {
        private EntityTestFixture _fixture;
        private readonly HelloHomeDbContext _dbCtx;
        private readonly PortGroup _portGroup;

        private const long BaseSignature = 76587486538549;
        private const byte TestNetwork = 66;

        public CreateTriggerTests(EntityTestFixture fixture)
        {
            _fixture = fixture;
            _dbCtx = fixture.DbCtx;
            _portGroup = new PushSensorGroup() { Name = "Test", Ports = new List<PushSensor>()};
            _dbCtx.PortGroups.Add(_portGroup);
            _dbCtx.SaveChanges();
        }

        [Fact]
        public void create_pulsetrigger()
        {
            var trigger  = new PushTrigger
            {
                PortGroup = _portGroup,
            };
            _dbCtx.Triggers.Add(trigger);
            _dbCtx.SaveChanges();
        }

        [Fact]
        public void create_switchTrigger()
        {
            var trigger  = new SwitchTrigger
            {
                PortGroup = _portGroup,
                TriggerOnState = true,
            };
            _dbCtx.Triggers.Add(trigger);
            _dbCtx.SaveChanges();
        }

        [Fact]
        public void create_varioTrigger()
        {
            var trigger  = new VarioTrigger
            {
                PortGroup = _portGroup,
            };
            _dbCtx.Triggers.Add(trigger);
            _dbCtx.SaveChanges();
        }

        [Fact]
        public void create_cronTrigger()
        {
            var trigger  = new CronTrigger
            {
                CronExpression = "* * * 8"
            };
            _dbCtx.Triggers.Add(trigger);
            _dbCtx.SaveChanges();
        }
    }
}