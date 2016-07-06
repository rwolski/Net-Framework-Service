using Autofac;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public abstract class ServiceData : IServiceData
    {
        //public int Val { get; set; }

        public ILifetimeScope Scope { get; set; }

        protected ServiceData()
        {
        }

        public abstract Task Action();
    }
}