using System;
using System.Data.Entity;
using HelloHome.Common.Entities;
using System.Linq;
using System.Threading;
using Castle.Windsor;
using HelloHome.NetGateway.WindsorInstallers;
using NLog;
using Timer = System.Timers.Timer;
using System.Collections.Generic;

namespace HelloHome.NetGateway
{
	class MainClass
	{
		static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		public static void Main (string[] args)
		{
			Logger.Info ("Starting gateway...");
		    try
		    {
		        Database.SetInitializer<HelloHomeDbContext>(null);

		        var container = new WindsorContainer();
		        container.Install(new DefaultInstaller());

				var dbContext = container.Resolve<HelloHomeDbContext> ("TransientDbContext");
				dbContext.SaveChanges ();
				Logger.Info ($"{dbContext.Nodes.Count ()} nodes found in the database");
				container.Release (dbContext);

				try {
					container.Resolve<HelloHomeGateway> ();
				} catch (Exception ex) { 
					Logger.Error (ex.Message);
					return;
				}
				  
		        var emonCmsUpdater = container.Resolve<IEMonCmsUpdater>();
		        var timer = new Timer(20000) {AutoReset = true};
		        timer.Elapsed += (sender, e) => emonCmsUpdater.Update();
		        //timer.Enabled = true;

				Console.ReadLine ();
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
