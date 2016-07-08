using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public interface IMessageResponse<TRequest>
    {
        TRequest Request { get; }

        Task<object> Response();
    }
}
