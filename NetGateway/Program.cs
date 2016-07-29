using System;
using System.Data.Entity;
using HelloHome.Common.Entities;
using System.Linq;
using HelloHome.NetGateway.WindsorInstallers;
using NLog;


namespace HelloHome.NetGateway
{
	class MainClass
	{
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Main (string[] args)
		{
			Logger.Info ("Starting gateway...");
			Database.SetInitializer<HelloHomeDbContext> (null);

			var container = new Castle.Windsor.WindsorContainer ();
			container.Install (new DefaultInstaller());

			var dbContext = container.Resolve<HelloHomeDbContext> ("PipelineFreeDbContext");
			Console.WriteLine ($"Node count : {dbContext.Nodes.Count()}");

			container.Resolve<HelloHomeGateway> ();

			var emonCmsUpdater = container.Resolve<IEMonCmsUpdater> ();
			var timer = new System.Timers.Timer (20000) { AutoReset = true };
			timer.Elapsed += (sender, e) => emonCmsUpdater.Update ();
			//timer.Enabled = true;

			while (true)
				System.Threading.Thread.Sleep (100);
		}
	}
}
