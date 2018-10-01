using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Alexa.Skill
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "CoreTemperatureOverview",
				url: "Kerntemperaturen/{skillId}",
				defaults: new { controller = "CoreTemperature", action = "Overview" }
			);

			routes.MapRoute(
				name: "CoreTemperatureEdit",
				url: "Kerntemperaturen/Bearbeiten/{skillId}/{temperatureId}",
				defaults: new { controller = "CoreTemperature", action = "EditTemperature", temperatureId = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
