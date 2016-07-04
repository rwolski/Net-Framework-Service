using Microsoft.AspNet.SignalR;
using System;

namespace Framework.WebSockets
{
    public class SignalRProvider : ISocketProvider
    {
        public SignalRProvider()
        {
        }

        public IHubHost GetHub()
        {
            return new HubHost(GlobalHost.ConnectionManager.GetHubContext<SignalRHub>());
        }

        public IHubHost GetHub(string hubName)
        {
            return new HubHost(GlobalHost.ConnectionManager.GetHubContext(hubName));
        }
    }
}
