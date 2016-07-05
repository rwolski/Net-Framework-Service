using System;
using System.Threading.Tasks;

namespace Framework.Queue
{
    public interface IServiceBus<T> : IDisposable
    {
        Task Send(T message);

        Task Publish(T message);

    }
}
