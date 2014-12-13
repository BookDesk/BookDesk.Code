using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BuySellOldBooks
{
    public class RouteConfig
    {
        //Specify URL pattern here to match with incoming request and to redirect to any aspx(if req).
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Book", action = "SearchBooks", id = UrlParameter.Optional }
            );
        }
    }
}