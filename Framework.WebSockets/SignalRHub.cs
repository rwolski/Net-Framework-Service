using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Framework.WebSockets
{
    public class SignalRHub : Hub //<IHubClient>
    {
        IHubHost _host;

        public SignalRHub(IHubHost client)
        {
            _host = client;
        }

        #region Broadcast to all

        //public Task Broadcast(string name, object message)
        //{
        //    return Clients.Others.sendMessage(name, JsonConvert.SerializeObject(message));
        //}

        //public Task Broadcast(string method, string name, object message)
        //{
        //    IClientProxy proxy = Clients.All;
        //    return proxy.Invoke(method, name, JsonConvert.SerializeObject(message));
        //}

        public Task Broadcast(string name, object message)
        {
            return _host.Broadcast(name, message, Context.ConnectionId);
        }

        public Task Broadcast(string method, string name, object message)
        {
            return _host.Broadcast(method, name, message, Context.ConnectionId);
        }

        #endregion

        #region Send to one

        //public Task SendToClient(string connection, string name, object message)
        //{
        //    return Clients.Client(connection).sendMessage(name, JsonConvert.SerializeObject(message));
        //}

        // public Task SendToClient(string method, string connection, string name, object message)
        //{
        //    IClientProxy proxy = Clients.Client(connection);
        //    return proxy.Invoke(method, name, JsonConvert.SerializeObject(message));
        //}

        public Task SendToClient(string connection, string name, object message)
        {
            return _host.SendToClient(connection, name, message);
        }

        public Task SendToClient(string method, string connection, string name, object message)
        {
            return _host.SendToClient(method, connection, name, message);
        }

        #endregion

        #region Send to group

        //public Task SendToGroup(string group, string name, object message)
        //{
        //    return Clients.OthersInGroup(group).sendMessage(name, JsonConvert.SerializeObject(message));
        //}

        //public Task SendToGroup(string method, string group, string name, object message)
        //{
        //    IClientProxy proxy = Clients.OthersInGroup(group);
        //    return proxy.Invoke(method, name, JsonConvert.SerializeObject(message));
        //}

        public Task SendToGroup(string group, string name, object message)
        {
            return _host.SendToGroup(group, name, message);
        }

        public Task SendToGroup(string method, string group, string name, object message)
        {
            return _host.SendToGroup(method, group, name, message);
        }

        #endregion

        public void JoinGroup(string group)
        {
            Groups.Add(Context.ConnectionId, group);
        }

        public void LeaveGroup(string group)
        {
            Groups.Remove(Context.ConnectionId, group);
        }
    }
}
