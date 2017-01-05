using System;
using Xunit;

namespace End2EndTests.Scenarios
{
    public class ScenarioBase
    {
        [Fact]
        public void BaseTest()
        {
            throw new Exception(this.GetType().Assembly.CodeBase);
        }
    }
}