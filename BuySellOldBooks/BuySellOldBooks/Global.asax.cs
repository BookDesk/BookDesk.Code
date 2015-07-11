using BuySellOldBooks.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

//Added on 11-July-2015  for baq
namespace BuySellOldBooks
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
    //        System.Net.ServicePointManager.ServerCertificateValidationCallback +=
    //(s, cert, chain, sslPolicyErrors) => true;
            //Database initilization stretegy. Seed the database with data.

            // Summary:
            //     Gets or sets the database initialization strategy. The database initialization
            //     strategy is called when System.Data.Entity.DbContext instance is initialized
            //     from a System.Data.Entity.Infrastructure.DbCompiledModel. The strategy can
            //     optionally check for database existence, create a new database, and seed
            //     the database with data.  The default strategy is an instance of System.Data.Entity.CreateDatabaseIfNotExists<TContext>.
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BuySellOldBooks.DataAccessLayer.PustakContext, Configuration>());

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            
        }
    }
}