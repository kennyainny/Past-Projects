using Application.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Application.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            //System.Data.Entity.Database.SetInitializer<ApplicationDatabaseContext>(
            //    new System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDatabaseContext>());


            //System.Data.Entity.Database.SetInitializer(new Application.Web.Models.DatabaseContextInitializer());

            //if (!Roles.RoleExists("Admin"))   
            //         Roles.CreateRole("Admin");
            
            //if (!Roles.RoleExists("User"))
            //    Roles.CreateRole("User"); 

            //var myDb = new ApplicationDatabaseContext();
            //myDb.Database.Initialize(true);

            //protected override void Seed(UsersContext context)  
             //{        
             //    WebSecurity.InitializeDatabaseConnection(  
             //        "DefaultConnection",  "UserProfile",   "UserId",   "UserName", autoCreateTables: true);  
             //    if (!Roles.RoleExists("Administrator"))   
             //        Roles.CreateRole("Administrator"); 
             //}
        }
    }
}