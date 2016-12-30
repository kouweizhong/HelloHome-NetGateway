using System.Linq;
using System.Threading;
using HelloHome.Common.Entities;
using HelloHome.NetGateway;
using HelloHome.NetGateway.Agents.NodeGateway;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using Moq;
using Xunit;

namespace End2EndTests.Scenarios
{
    public class NodeStartupScenario : IClassFixture<TestableGateway>
    {
        private readonly HelloHomeDbContext _dbCtx;
        private readonly Mock<INodeMessageChannel> _msgChannel;
        private readonly NodeGateway _gtw;

        public NodeStartupScenario(TestableGateway testableGateway)
        {
            _dbCtx = testableGateway.DbContext;
            _msgChannel = testableGateway.MessageReader;
            _gtw = testableGateway.Gateway;
            _dbCtx.Database.Delete();
            _dbCtx.Database.Create();
        }

        [Fact]
        public async void nodes_are_created_the_first_time_they_stertup()
        {
            //Arrange
            var cts = new CancellationTokenSource();
            var startupMessage = new NodeStartedReport {FromNodeId = 12, Major = 1, Minor = 2};

            _msgChannel.Setup(_ => _.ReadAsync(cts.Token)).ReturnsAsync(startupMessage);

            //Act
            await _gtw.RunOnceAsync(cts.Token, true);

            //Assert
            var expectedNode = _dbCtx.Nodes.SingleOrDefault(_ => _.RfAddress == 12);
            Assert.NotNull(expectedNode);
        }

        [Fact]
        public async void no_config_command_if_node_has_unique_rfAddress()
        {
            //Arrange
            var cts = new CancellationTokenSource();
            var startupMessage = new NodeStartedReport {FromNodeId = 12, Major = 1, Minor = 2};

            _msgChannel.Setup(_ => _.ReadAsync(cts.Token)).ReturnsAsync(startupMessage);

            //Act
            await _gtw.RunOnceAsync(cts.Token, true);

            //Assert
            _msgChannel.Verify(_ => _.SendAsync(It.IsAny<NodeConfigCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async void new_rfaddress_is_suggested_if_already_in_use()
        {
            //Arrange
            _dbCtx.Nodes.Add(new Node {RfAddress = 12});
            _dbCtx.SaveChanges();
            var cts = new CancellationTokenSource();
            var startupMessage = new NodeStartedReport {FromNodeId = 12, Major = 1, Minor = 2};
            _msgChannel.Setup(_ => _.ReadAsync(cts.Token)).ReturnsAsync(startupMessage);

            //Act
            await _gtw.RunOnceAsync(cts.Token, true);

            //Assert
            _msgChannel.Verify(_ => _.SendAsync(It.Is<NodeConfigCommand>(c => c.NewRfAddress != 12), It.IsAny<CancellationToken>()));
        }
    }
}