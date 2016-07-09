using System;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public interface IMessageRequestHandler<TReq>
    {
        Task<object> Request(TReq request);
    }
}
