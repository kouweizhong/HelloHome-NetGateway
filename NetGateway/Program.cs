using System;
using HelloHome.NetGateway.Configuration.AppSettings;
using Castle.MicroKernel.Registration;
using HelloHome.NetGateway.Configuration;
using System.Data.Entity;
using HelloHome.Common.Entities;
using System.Linq;
using HelloHome.NetGateway.Agents.NodeGateway;
using System.Collections.Generic;
using HelloHome.NetGateway.Processors;
using HelloHome.NetGateway.WindsorInstallers;
using log4net;
using HelloHome.NetGateway.Pipeline;


namespace HelloHome.NetGateway
{
	class MainClass
	{
		static readonly ILog log = LogManager.GetLogger(typeof(MainClass).FullName);
			
		public static void Main (string[] args)
		{
			log.Info ("Starting gateway...");
			Database.SetInitializer<HelloHomeDbContext> (null);

			var container = new Castle.Windsor.WindsorContainer ();
			container.Install (new DefaultInstaller (typeof(NodeGatewayAgent)));

			var dbContext = container.Resolve<HelloHomeDbContext> ("PipelineFreeDbContext");
			Console.WriteLine ($"Node count : {dbContext.Nodes.Count()}");

			container.Resolve<HelloHomeGateway> ();

			var emonCmsUpdater = container.Resolve<EmonCmsUpdater> ();
			var timer = new System.Timers.Timer (20000) { AutoReset = true };
			timer.Elapsed += (sender, e) => emonCmsUpdater.Update ();
			//timer.Enabled = true;

			while (true)
				System.Threading.Thread.Sleep (100);
		}
	}
}
