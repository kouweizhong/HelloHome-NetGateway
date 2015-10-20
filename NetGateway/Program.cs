using System;
using NetHhGateway.Configuration.AppSettings;
using Castle.MicroKernel.Registration;
using NetHhGateway.Configuration;
using System.Data.Entity;
using NetHhGateway.Entities;
using System.Linq;
using NetHhGateway.Agents.NodeGateway;
using System.Collections.Generic;
using NetHhGateway.Processors;
using NetHhGateway.WindsorInstallers;


namespace NetHhGateway
{
	class MainClass
	{

		public static void Main (string[] args)
		{
			Console.WriteLine ("Starting gateway...");
			Database.SetInitializer<HelloHomeDbContext> (null);

			var container = new Castle.Windsor.WindsorContainer ();
			container.Install (new DefaultInstaller (typeof(FakeNodeGatewayAgent)));

			Console.WriteLine ("Node count : {0}", container.Resolve<HelloHomeDbContext> ().Nodes.Count ());

			var gatewayPipeline = container.Resolve<GatewayPipeline> ();

			gatewayPipeline.StartPipeline ();

			var emonCmsUpdater = container.Resolve<EmonCmsUpdater> ();
			var timer = new System.Timers.Timer (20000) { AutoReset = true };
			timer.Elapsed += (sender, e) => emonCmsUpdater.Update ();
			timer.Enabled = true;

			while (true)
				System.Threading.Thread.Sleep (100);
		}
	}
}
