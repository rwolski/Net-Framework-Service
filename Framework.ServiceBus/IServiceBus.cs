using System;
using System.Threading.Tasks;

namespace Framework.Queue
{
    public interface IServiceBus<T> : IDisposable where T : class
    {
        Task Send(T message);

        Task Publish(T message);

    }
}
