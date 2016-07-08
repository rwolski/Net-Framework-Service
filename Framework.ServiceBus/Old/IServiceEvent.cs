using Autofac;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public interface IServiceEvent
    {
        ILifetimeScope Scope { get; set; }

        Task Action();
    }
}