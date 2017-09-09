﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Wiki
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            //routes.MapRoute(
            //    name: "Wiki",
            //    url: "Wiki/{titre}/{action}",
            //    defaults: new { controller = "Wiki", action = "Index", titre = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{titre}",
                defaults: new { controller = "Home", action = "Index", titre = UrlParameter.Optional }
            );
        }
    }
}
