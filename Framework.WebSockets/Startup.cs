using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("WebSockets", typeof(Framework.WebSockets.Startup))]

namespace Framework.WebSockets
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.HubPipeline.AddModule(new SignalRErrorHandling());
            //GlobalHost.DependencyResolver = new DefaultDependencyResolver();

            var config = new HubConfiguration();
            config.EnableJavaScriptProxies = true;
            config.EnableDetailedErrors = true;

            app.MapSignalR("/sockets", config);
        }
    }
}
