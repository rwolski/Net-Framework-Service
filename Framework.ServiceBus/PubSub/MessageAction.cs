using Autofac;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public abstract class MessageAction<TContract> : IMessageAction<TContract> where TContract : class
    {
        public TContract Contract { get; protected set; }

        public MessageAction(TContract contract)
        {
            Contract = contract;
        }

        public abstract Task Action();
    }
}