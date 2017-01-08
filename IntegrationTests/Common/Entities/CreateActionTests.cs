using HelloHome.Common.Entities;
using Xunit;

namespace IntegrationTests.Common.Entities
{
    public class CreateActionTests : IClassFixture<EntityTestFixture>
    {
        private readonly EntityTestFixture _fixture;
        private HelloHomeDbContext _dbCtx;

        public CreateActionTests(EntityTestFixture fixture)
        {
            _fixture = fixture;
            _dbCtx = fixture.DbCtx;
        }

        [Fact]
        public void CreateEmptyAction()
        {
            _dbCtx.Actions.Add(new TurnOnAction
            {
                Sequence = 1,
                Trigger = new PushTrigger { PortGroup = new PushSensorGroup { Name = "Boutons salon" }},
                RelayActuatorGroup = new RelayActuatorGroup { Name = "Lampes du salon"}
            });
            _dbCtx.Commit();
        }
    }
}