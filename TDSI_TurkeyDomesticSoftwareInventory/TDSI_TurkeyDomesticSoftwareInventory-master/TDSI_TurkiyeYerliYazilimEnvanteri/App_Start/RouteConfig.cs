using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TDSI_TurkiyeYerliYazilimEnvanteri
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index"}
            );

            routes.MapRoute(
                   name: "Software",                                              // Route name
                   url: "{controller}/{action}",                           // URL with parameters
                   defaults: new { controller = "Softwares", action = "Software"}  // Parameter defaults
               );

            routes.MapRoute(
                   name: "Company",                                              // Route name
                   url: "{controller}/{action}",                           // URL with parameters
                   defaults: new { controller = "Companies", action = "Company" }  // Parameter defaults
               );

            routes.MapRoute(
                   name: "User",                                              // Route name
                   url: "{controller}/{action}",                           // URL with parameters
                   defaults: new { controller = "Users" }  // Parameter defaults
               );
        }
    }
}
