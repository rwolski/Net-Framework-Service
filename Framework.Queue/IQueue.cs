using System;
using System.Threading.Tasks;

namespace Framework.Queue
{
    public interface IQueue<T> : IDisposable
    {
        Task Send(T message);

        Task Publish(T message);

        Task<T> Receive();

        Task<T> Consume();

        Task<IMessageResponse<T>> Request(IMessageRequest<T> request = null);
    }
}
