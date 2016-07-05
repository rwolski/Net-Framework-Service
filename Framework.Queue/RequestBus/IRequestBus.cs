using System;
using System.Threading.Tasks;

namespace Framework.Queue
{
    public interface IRequestBus<T> : IDisposable
    {
        Task<IMessageResponse<T>> Request(IMessageRequest<T> request);

    }
}
