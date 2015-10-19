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
			
			Database.SetInitializer<NetHhGateway.Entities.HelloHomeDbContext> (null);

			var container = new Castle.Windsor.WindsorContainer ();
			container.Install (new DefaultInstaller (typeof(NodeGatewayAgent)));

			Console.WriteLine ("Node count : {0}" , container.Resolve<HelloHomeDbContext> ().Nodes.Count ());

			var gatewayPipeline = container.Resolve<GatewayPipeline> ();

			gatewayPipeline.StartPipeline ();
			while (true);
		}
	}
}
