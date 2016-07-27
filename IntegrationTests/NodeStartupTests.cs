using System;
using HelloHome.NetGateway;
using HelloHome.NetGateway.Agents.NodeGateway;
using NUnit.Framework;

namespace IntegrationTests
{
	public class NodeStartupTests
	{
		readonly FakeNodeGatewayAgent _nodeAgent;
		readonly HelloHomeGateway _sut;


		public NodeStartupTests ()
		{
			_nodeAgent = new FakeNodeGatewayAgent ();
			var container = new Castle.Windsor.WindsorContainer ();
			container.Install (new HelloHome.NetGateway.WindsorInstallers.DefaultInstaller (_nodeAgent));
			_sut = container.Resolve<HelloHomeGateway> ();
		}

		[Test]
		public void CreateNodeIfNotYetExists ()
		{
			
		}
	}
}

