using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public interface IMessageAction<TContract>
    {
        TContract Contract { get; }

        Task Action();
    }
}
