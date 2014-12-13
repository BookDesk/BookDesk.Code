using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BuySellOldBooks
{
    public static class WebApiConfig
    {
        //Ragister route and URL pattern here for WebApi
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
