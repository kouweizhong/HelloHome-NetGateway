using System;
using System.Data.Entity;
using HelloHome.Common.Entities;
using System.Linq;
using System.Threading;
using Castle.Windsor;
using HelloHome.NetGateway.WindsorInstallers;
using NLog;
using Timer = System.Timers.Timer;


namespace HelloHome.NetGateway
{
	class MainClass
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		public static void Main (string[] args)
		{
			Logger.Info ("Starting gateway...");
		    try
		    {
		        Database.SetInitializer<HelloHomeDbContext>(null);

		        var container = new WindsorContainer();
		        container.Install(new DefaultInstaller());

		        container.Resolve<HelloHomeGateway>();

		        var emonCmsUpdater = container.Resolve<IEMonCmsUpdater>();
		        var timer = new Timer(20000) {AutoReset = true};
		        timer.Elapsed += (sender, e) => emonCmsUpdater.Update();
		        //timer.Enabled = true;

		        while (true)
		            Thread.Sleep(100);
		    }
		    catch (Exception e)
		    {
		        Logger.Fatal(e);		        
		    }
		    finally
		    {
                LogManager.Flush();
            }
		}
	}
}
