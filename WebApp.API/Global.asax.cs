using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApp.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //SwaggerConfig.Register();

            MongoConfig.RegisterTypes();
            ContainerConfig.Build(ContainerConfig.RegisterModules());
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            //if (!System.Diagnostics.EventLog.SourceExists
            //("ASPNETApplication"))
            //{
            //    System.Diagnostics.EventLog.CreateEventSource
            //       ("ASPNETApplication", "Application");
            //}
            //System.Diagnostics.EventLog.WriteEntry
            //    ("ASPNETApplication",
            //    Server.GetLastError().Message);
        }

        protected void Application_BeginRequest()
        {

        }

        protected void Application_EndRequest()
        {

        }
    }
}
