using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProductsMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(name: null,
                url: "Page/{page}",
                defaults: new { Controller = "Product", action = "List" });
            /*
            routes.MapRoute(
               name: "",
               url: "{controller}/{action}/{searchString}"
               );*/

            routes.MapRoute(
                name: "Default",
                url: "{locale}/{controller}/{action}/{id}",
                defaults: new { locale = "sv", controller = "Products", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}
