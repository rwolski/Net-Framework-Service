using Autofac;
using Autofac.Core;
using MassTransit;

namespace Framework.ServiceBus
{
    public class ServiceBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ServiceBusModule).Assembly)
                .Where(t =>
                {
                    var a = typeof(IConsumer).IsAssignableFrom(t);
                    return a;
                })
                .AsSelf();
        }
    }
}