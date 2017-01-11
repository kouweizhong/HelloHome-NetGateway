using HelloHome.Common.Entities;
using Xunit;

namespace IntegrationTests.Common.Entities
{
    public class CreateActionTests : EntityTest
    {

        [Fact]
        public void CreateEmptyAction()
        {
            DbCtx.Actions.Add(new TurnOnAction
            {
                Sequence = 1,
                Trigger = new CronTrigger { CronExpression = "* * * 12"},
            });
            DbCtx.Commit();
        }
    }
}