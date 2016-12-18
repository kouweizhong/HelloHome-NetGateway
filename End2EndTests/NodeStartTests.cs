using System;
using System.Linq;
using System.Data.Entity;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using Moq;
using Xunit;
using HelloHome.NetGateway.Commands;
using Common.Extentions;

namespace End2EndTests
{
	public class NodeStartTests : TestableGateway
	{
		[Fact(Skip = "Not know")]
		public void Node_Has_Valid_RfId_And_Does_Not_Requires_NewRdId_But_does_Not_Exists_InDb ()
		{
			//Arrange
			var tp = Mock<ITimeProvider> ();
			var now = DateTime.UtcNow.Round(TimeSpan.FromSeconds (1));
			tp.SetupGet (_ => _.UtcNow).Returns (now);

			//Act
			Gateway.MessageReceived (this, new NodeStartedReport { NeedNewRfAddress = false, FromNodeId = 1, Signature = 1234, Major=1, Minor=0 });

			//Assert
			NodeAgent.Verify (_ => _.Send (It.IsAny<NodeConfigCommand> ()), Times.Never);

			var node = DbContext.Nodes
								.Include (_ => _.NodeFacts)
								.Include (_ => _.Configuration)
								.SingleOrDefault (_ => _.Signature == 1234);
			Assert.Equal (1, node.RfId);
			Assert.Equal (1234, node.Signature);
			Assert.Equal ("1.0", node.NodeFacts.Version);
			Assert.Equal (now, node.NodeFacts.LastStartupTime);
			Assert.Equal (0, node.NodeFacts.MaxUpTime);
			NodeAgent.Verify (_ => _.Send (It.IsAny<NodeConfigCommand> ()), Times.Never ());
		}

		[Fact (Skip = "Not now")]
		public void NothingSentBackIfNodeExistsAndDontNeedNewNodeId ()
		{
			Gateway.MessageReceived (this, new NodeStartedReport { FromNodeId=12, Signature = 4567 });

			NodeAgent.Verify (_ => _.Send (It.IsAny<NodeConfigCommand> ()), Times.Never);
		}
}
}
