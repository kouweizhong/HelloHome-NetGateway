using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HelloHome.Common;
using HelloHome.Common.Entities;
using HelloHome.NetGateway;
using HelloHome.NetGateway.Agents.NodeGateway;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using Moq;
using NLog;
using Xunit;
using Xunit.Abstractions;

namespace End2EndTests.Scenarios
{
    public class NodeStartupScenario : IClassFixture<TestableGateway>
    {
        private readonly TestableGateway _testableGateway;
        private readonly HelloHomeDbContext _dbCtx;
        private readonly Mock<INodeMessageChannel> _msgChannel;
        private readonly NodeGateway _gtw;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public NodeStartupScenario(ITestOutputHelper output, TestableGateway testableGateway)
        {
            _testableGateway = testableGateway;
            _dbCtx = testableGateway.DbCtx;
            _msgChannel = new Mock<INodeMessageChannel>();
            _gtw = testableGateway.CreateGateway(_msgChannel.Object);
            Logger.Debug("Gateway created with channel {0}", _msgChannel.GetHashCode());
        }

        [Fact]
        public async Task nodes_are_created_the_first_time_they_startup()
        {
            Logger.Debug("Start test {0}", nameof(nodes_are_created_the_first_time_they_startup));
            //Arrange
            var rfId = _testableGateway.GetNextRfId();
            var cts = new CancellationTokenSource();
            var startupMessage = new NodeStartedReport {FromNodeId = rfId, Major = 1, Minor = 2, Signature = 1};

            _msgChannel.Setup(_ => _.ReadAsync(cts.Token)).ReturnsAsync(startupMessage);

            //Act
            await _gtw.RunOnceAsync(cts.Token, true);

            //Assert
            var expectedNode = _dbCtx.Nodes.SingleOrDefault(_ => _.Signature == 1);
            Assert.NotNull(expectedNode);
        }

        [Fact]
        public async Task no_config_command_if_node_has_unique_rfAddress()
        {
            Logger.Debug("Start test {0}", nameof(no_config_command_if_node_has_unique_rfAddress));

            //Arrange
            var rfId = _testableGateway.GetNextRfId();
            var cts = new CancellationTokenSource();
            var startupMessage = new NodeStartedReport {FromNodeId = rfId, Major = 1, Minor = 2, Signature = 2};

            _msgChannel.Setup(_ => _.ReadAsync(cts.Token)).ReturnsAsync(startupMessage);

            //Act
            await _gtw.RunOnceAsync(cts.Token, true);

            //Assert
            _msgChannel.Verify(_ => _.SendAsync(It.IsAny<NodeConfigCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task new_rfaddress_is_suggested_if_already_in_use()
        {
            Logger.Debug("Start test {0}", nameof(new_rfaddress_is_suggested_if_already_in_use));

            //Arrange
            var rfId = _testableGateway.GetNextRfId();
            _dbCtx.Nodes.Add(new Node {RfAddress = rfId, RfNetwork = Constants.NetworkId,  Signature = 3, Configuration = new NodeConfiguration(), LatestValues = new LatestValues()});
            _dbCtx.Commit();

            var cts = new CancellationTokenSource();
            var startupMessage = new NodeStartedReport {FromNodeId = rfId, Major = 1, Minor = 2, Signature = 4};
            _msgChannel.Setup(_ => _.ReadAsync(cts.Token)).ReturnsAsync(startupMessage);

            //Act
            await _gtw.RunOnceAsync(cts.Token, true);

            //Assert
            _msgChannel.Verify(_ => _.SendAsync(It.Is<NodeConfigCommand>(c => c.signature == 4 && c.NewRfAddress != rfId), It.IsAny<CancellationToken>()));
        }
    }
}