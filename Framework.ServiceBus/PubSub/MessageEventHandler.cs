using Autofac;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public abstract class MessageEventHandler<TContract> : IMessageEventHandler<TContract> where TContract : class
    {
        public TContract Contract { get; protected set; }

        public MessageEventHandler(TContract contract)
        {
            Contract = contract;
        }

        public abstract Task Handle();
    }
}