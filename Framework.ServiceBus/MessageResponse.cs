using Autofac;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public abstract class MessageResponse<TRequest> : IMessageResponse<TRequest>
        where TRequest : class
    {
        public TRequest Request { get; protected set; }

        public MessageResponse(TRequest request)
        {
            Request = request;
        }

        public abstract Task<object> Response();
    }
}