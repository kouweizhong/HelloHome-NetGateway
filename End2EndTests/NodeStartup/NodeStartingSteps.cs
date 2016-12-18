using System;
using System.Linq;
using System.Data.Entity;
using HelloHome.Common.Entities;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.Commands;
using Moq;
using TechTalk.SpecFlow;
using Xunit;
using Common.Extentions;

namespace End2EndTests
{
	[Binding]
	public class NodeStartingSteps
	{
		readonly TestableGateway testableGateway;

		Mock<ITimeProvider> _timeProvider;

		DateTime _now;

		long? _signature = 0;
		byte? _rfId = 0;
		bool _needNewRf = false;

		Node nodeFromDbWithMySignature {
			get {
				return testableGateway.DbContext.Nodes .AsNoTracking ().Include (_ => _.LatestValues).SingleOrDefault (_ => _.Id == _signature.Value);
			}
		}


		public NodeStartingSteps (TestableGateway scenario)
		{
			this.testableGateway = scenario;
			_timeProvider = scenario.Mock<ITimeProvider> ();
			_now = DateTime.UtcNow.Round (TimeSpan.FromSeconds (1));
			_timeProvider.SetupGet (_ => _.UtcNow).Returns (_now);
		}

		[Given ("that my unique signature is (.*)")]
		public void MyUniqueSignatureIs (long signature)
		{
			_signature = signature;
		}

		[Given ("that my rfId is (.*)")]
		public void MyMyRfIs (byte rfId)
		{
			_rfId = rfId;
		}

		[Given ("that I start for the first time")]
		public void IStartForTheFirstTime ()
		{
			Assert.True (_signature.HasValue, "Signature not set");
			Assert.False (testableGateway.DbContext.Nodes.Any (_ => _.Id == _signature.Value));
		}

		[Given ("that my rfId is not yet known by the gateway")]
		public void IUseRfIdNotYetKnown ()
		{
			Assert.True (_rfId.HasValue, "rfId not set");
			Assert.False (testableGateway.DbContext.Nodes.Any (_ => _.RfAddress == _rfId), "rfId found in database");
		}

		[Given ("I need a new rfId")]
		public void INeedANewRfId ()
		{
			_needNewRf = true;
		}

		[Given ("that I'm already known by the gateway")]
		public void ImAlreadyKnownByTheGateway ()
		{
			Assert.NotNull (_signature);
			testableGateway.DbContext.Nodes.Add (new Node { Signature = _signature.Value, LatestValues = new LatestValues { StartupTime = _now.AddHours (-8) }, Configuration = new NodeConfiguration { Version = "1.0" } });
			testableGateway.DbContext.Commit ();
		}

		[When ("I start")]
		public void WhenIStart ()
		{
			Assert.True (_signature.HasValue);
			testableGateway.Gateway.MessageReceived (this, new NodeStartedReport {
				FromNodeId = _rfId ?? (byte)0,
				Signature = _signature.Value,
				NeedNewRfAddress = _needNewRf
			});
		}

		[Then ("A new node is created with my signature and rfId (.*)")]
		public void ANewNodeIsCreatedWithMySignatureAndRfId (byte rfId)
		{
			Assert.NotNull (nodeFromDbWithMySignature);
			Assert.Equal (_signature, nodeFromDbWithMySignature.Id);
			Assert.Equal (_rfId, nodeFromDbWithMySignature.RfAddress);
		}

		[Then ("A new node is created with my signature")]
		public void ANewNodeIsCreatedWithMySignature ()
		{
			Assert.NotNull (nodeFromDbWithMySignature);
			Assert.Equal (_signature, nodeFromDbWithMySignature.Id);
		}

		[Then ("No new config message is sent back")]
		public void NoNewConfigMessageIsSent ()
		{
			testableGateway.NodeAgent.Verify (_ => _.Send (It.IsAny<NodeConfigCommand> ()), Times.Never);
		}

		[Then ("A new config message is sent back witht the new rfId")]
		public void ANewConfigMessageIsSent ()
		{
			testableGateway.NodeAgent.Verify (_ => _.Send (It.Is<NodeConfigCommand> (m => m.NewRfAddress == nodeFromDbWithMySignature.RfAddress && m.signature == _signature)));
		}

		[Then (@"Last startup time is updated")]
		public void ThenLastStartupTimeIsUpdated ()
		{
			Assert.Equal (_now, nodeFromDbWithMySignature.LatestValues.StartupTime);
		}
	}
}
