using Autofac;
using MassTransit;
using System;
using System.Linq;

namespace Framework.ServiceBus
{
    public class ServiceBusModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register the generic message consumer
            builder.RegisterGeneric(typeof(MessageConsumer<>)).AsSelf();

            // Register all message types derived from IServiceData
            var consumerTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IServiceEvent).IsAssignableFrom(p) && !p.IsAbstract);

            foreach (var type in consumerTypes)
            {
                builder.RegisterType(type);
                builder.RegisterType(typeof(MessageConsumer<>).MakeGenericType(type));
            }
        }
    }
}