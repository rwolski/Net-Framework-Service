using System.Threading.Tasks;

namespace Framework.WebSockets
{
    public interface IHubHost
    {
        Task Broadcast(string name, object message, string excludedConnection = null);

        Task Broadcast(string method, string name, object message, string excludedConnection = null);

        Task SendToClient(string connection, string name, object message);

        Task SendToClient(string method, string connection, string name, object message);

        Task SendToGroup(string group, string name, object message, string excludedConnection = null);

        Task SendToGroup(string method, string group, string name, object message, string excludedConnection = null);
    }
}