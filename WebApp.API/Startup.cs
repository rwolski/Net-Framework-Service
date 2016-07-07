using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Autofac.Integration.WebApi;

[assembly: OwinStartup(typeof(WebApp.API.Startup))]

namespace WebApp.API
{
    public partial class Startup
    {
        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var builder = ContainerConfig.RegisterModules();
            var container = builder.Build();

            var config = GlobalConfiguration.Configuration;
            //var config = new HttpConfiguration();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container.BeginLifetimeScope());

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
    }
}
