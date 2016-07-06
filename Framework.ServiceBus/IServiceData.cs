using Autofac;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public interface IServiceData
    {
        //int Val { get; set; }

        ILifetimeScope Scope { get; set; }

        Task Action();
    }
}