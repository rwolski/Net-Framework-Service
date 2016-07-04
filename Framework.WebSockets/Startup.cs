using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Framework.WebSockets.Startup))]

namespace Framework.WebSockets
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.HubPipeline.AddModule(new SignalRErrorHandling());

            app.MapSignalR("/sockets", new HubConfiguration()
            {
                EnableDetailedErrors = true
            });
        }
    }
}
