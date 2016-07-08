using System;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public interface IServiceBus : IDisposable
    {
        Task Send<TData>(TData message, CancellationToken ct = default(CancellationToken))
            where TData : class;

        Task Publish<TData>(object message, CancellationToken ct = default(CancellationToken))
            where TData : class;

        Task Publish<TData>(TData message, CancellationToken ct = default(CancellationToken))
            where TData : class;

        Task<TData> Request<TReq, TData>(TReq request, CancellationToken ct = default(CancellationToken))
            where TReq : class, IMessageRequest
            where TData : class;

        Task<TData> Request<TReq, TData>(TReq request, string destination, CancellationToken ct = default(CancellationToken))
            where TReq : class, IMessageRequest
            where TData : class;
    }
}
