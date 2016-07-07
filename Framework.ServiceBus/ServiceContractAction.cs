using Autofac;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public class ServiceContractAction<TContract> : IServiceContractAction<TContract> where TContract : class
    {
        public TContract Contract { get; protected set; }

        public ServiceContractAction(TContract contract, ILifetimeScope scope)
        {
            Contract = contract;
        }

        public Task Action()
        {
            return Task.FromResult(0);
        }
    }
}