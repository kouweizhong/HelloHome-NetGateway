using System;
using NUnit.Framework;
using NetHhGateway;
using IntegrationTests.ScenarioBasedGatewayAgent;
using Castle.Windsor;
using NetHhGateway.WindsorInstallers;
using NetHhGateway.Agents.NodeGateway;
using NetHhGateway.Agents.NodeGateway.Domain;

namespace IntegrationTests
{
	[TestFixture]
	public class PulseTests
	{
		private readonly GatewayPipeline _pipeline;
		private readonly Scenario _scenario;

		public PulseTests ()
		{
			var container = new WindsorContainer ();	
			container.Install (new DefaultInstaller (typeof(Scenario)));
			_scenario = (Scenario)container.Resolve<INodeGatewayAgent> ();
			_pipeline = container.Resolve<GatewayPipeline> ();
		}

		[Test]
		public void simple_pulsereport_passes ()
		{
			_scenario.Send (new PulseReport { FromNodeId = 12, SubNode = 1, NewPulses = 1 });
			_pipeline.StartPipeline ();
			_scenario.WaitTillComplete ();
			_pipeline.StopPipeline ();
		}
	}
}
