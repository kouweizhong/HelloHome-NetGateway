using System;
using Owin;
using System.Web.Http;

namespace WebApi
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var webApiConfiguration = ConfigureWebApi();
			app.UseWebApi(webApiConfiguration);
		}

		private HttpConfiguration ConfigureWebApi()
		{
			var config = new HttpConfiguration();
			config.MapHttpAttributeRoutes();
			config.Routes.MapHttpRoute(
				"DefaultApi",
				"api/{controller}/{id}",
				new { id = RouteParameter.Optional });
			return config;
		}
	}
}

