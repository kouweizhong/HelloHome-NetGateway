using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace Scheduler
{
	class MainClass
	{
		public static void Main (string [] args)
		{
			RunProgram ().GetAwaiter ().GetResult ();
		}

		static async Task RunProgram ()
		{
			try {
				// Grab the Scheduler instance from the Factory
				NameValueCollection props = new NameValueCollection
				{
					{ "quartz.serializer.type", "binary" }
				};
				StdSchedulerFactory factory = new StdSchedulerFactory (props);
				IScheduler scheduler = await factory.GetScheduler ();

				// and start it off
				await scheduler.Start ();

				// some sleep to show what's happening
				await Task.Delay (TimeSpan.FromSeconds (10));

				// and last shut down the scheduler when you are ready to close your program
				await scheduler.Shutdown ();
			} catch (SchedulerException se) {
				await Console.Error.WriteLineAsync (se.ToString ());
			}
		}
	}
}
