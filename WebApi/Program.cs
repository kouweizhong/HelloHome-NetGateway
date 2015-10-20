using System;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using System.Data.Entity;
using HelloHome.Common.Entities;

namespace WebApi
{
	class MainClass
	{		
		static void Main(string[] args)
		{
			Database.SetInitializer<HelloHomeDbContext> (null);

			string baseUri = "http://*:5000";
			// in debug in visual studio you will need 
			//string baseUri = "http://localhost:8080";

			Console.WriteLine("Starting web Server...");
			WebApp.Start<Startup>(baseUri);
			Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);
			Console.WriteLine("I'm running on {0} directly from assembly {1}", Environment.OSVersion, System.Reflection.Assembly.GetEntryAssembly().FullName);
			Console.ReadLine();
		}
	}
}
