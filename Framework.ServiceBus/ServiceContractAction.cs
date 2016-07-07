using Autofac;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public abstract class ServiceContractAction<TContract> : IServiceContractAction<TContract> where TContract : class
    {
        public TContract Contract { get; protected set; }

        public ServiceContractAction(TContract contract)
        {
            Contract = contract;
        }

        public abstract Task Action();
    }
}