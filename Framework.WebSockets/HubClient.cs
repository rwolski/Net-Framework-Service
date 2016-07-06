using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Framework.WebSockets
{
    public class HubClient : IHubClient
    {
        IHubContext _context;

        public HubClient(IHubContext context)
        {
            _context = context;
        }

        public Task Broadcast(string name, object message, string excludedConnection = null)
        {
            if (string.IsNullOrWhiteSpace(excludedConnection))
                return _context.Clients.All.sendMessage(name, JsonConvert.SerializeObject(message));
            else
                return _context.Clients.AllExcept().sendMessage(name, JsonConvert.SerializeObject(message));
        }

        public Task Broadcast(string method, string name, object message, string excludedConnection = null)
        {
            IClientProxy proxy;
            if (string.IsNullOrWhiteSpace(excludedConnection))
                proxy = _context.Clients.All;
            else
                proxy = _context.Clients.AllExcept(excludedConnection);
            return proxy.Invoke(method, name, JsonConvert.SerializeObject(message));
        }

        public Task SendToClient(string connection, string name, object message)
        {
            return _context.Clients.Client(connection).sendMessage(name, JsonConvert.SerializeObject(message));
        }

        public Task SendToClient(string method, string connection, string name, object message)
        {
            IClientProxy proxy = _context.Clients.Client(connection);
            return proxy.Invoke(method, name, JsonConvert.SerializeObject(message));
        }

        public Task SendToGroup(string group, string name, object message, string excludedConnection = null)
        {
            if (!string.IsNullOrWhiteSpace(excludedConnection))
                return _context.Clients.Group(group, excludedConnection).sendMessage(name, JsonConvert.SerializeObject(message));
            else
                return _context.Clients.Group(group).sendMessage(name, JsonConvert.SerializeObject(message));
        }

        public Task SendToGroup(string method, string group, string name, object message, string excludedConnection = null)
        {
            IClientProxy proxy;
            if (!string.IsNullOrWhiteSpace(excludedConnection))
                proxy = _context.Clients.Group(group, excludedConnection);
            else
                proxy = _context.Clients.Group(group);
            return proxy.Invoke(method, name, JsonConvert.SerializeObject(message));
        }
    }
}
