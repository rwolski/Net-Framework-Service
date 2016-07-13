using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public interface IMessageEventHandler<TContract>
    {
        TContract Contract { get; }

        Task Handle();
    }
}
