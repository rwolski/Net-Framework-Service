using System;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public interface IServiceBus : IDisposable
    {
        Task Send<TData>(TData message) where TData : class;

        Task Publish<TData>(TData message) where TData : class;

    }
}
