using NUnit.Framework;
using System;
using Castle.Windsor;
using NetHhGateway.WindsorInstallers;
using IntegrationTests.ScenarioBasedGatewayAgent;
using NetHhGateway;
using NetHhGateway.Agents.NodeGateway.Domain;
using NetHhGateway.Agents.NodeGateway;
using System.Threading.Tasks;

namespace IntegrationTests
{
	[TestFixture ()]
	public class StartupTests
	{
		private readonly GatewayPipeline _pipeline;
		private readonly Scenario _scenario;
		
		public StartupTests ()
		{
			var container = new WindsorContainer ();	
			container.Install(new DefaultInstaller(typeof(Scenario)));
			_scenario = (Scenario)container.Resolve<INodeGatewayAgent> ();
			_pipeline = container.Resolve<GatewayPipeline> ();
		}

		[Test]
		public void simple_nodestartedreport_passes() {
			_scenario.Send (new NodeStartedReport { FromNodeId = 12, Major=0, Minor=1, NeedNewRfAddress = false, Signature=456789 });
			_pipeline.StartPipeline ();
			_scenario.WaitTillComplete ();
			_pipeline.StopPipeline ();
		}

	}
}

