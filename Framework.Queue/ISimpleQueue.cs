using System;
using System.Threading.Tasks;

namespace Framework.Queue
{
    public interface ISimpleQueue<T> : IDisposable
    {
        Task Send(T message);

        Task Publish(T message);

        Task<T> Receive();

        Task<T> Consume();
    }
}
