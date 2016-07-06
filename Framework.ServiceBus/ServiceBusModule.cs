using Autofac;
using MassTransit;
using System;
using System.Linq;
using System.Reflection;

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
                .Where(p => typeof(IServiceData).IsAssignableFrom(p) && !p.IsAbstract);

            foreach (var type in consumerTypes)
            {
                builder.RegisterType(type);
                builder.RegisterType(typeof(MessageConsumer<>).MakeGenericType(type));
            }

            
            
            //builder.var consumerTypes = builder.ReAssembly.GetExecutingAssemblyComponentRegistry.Registrations
            //            .Where(r => typeof(ServiceData).IsAssignableFrom(r.Activator.LimitType))
            //            .Select(r => r.Activator.LimitType)
            //            .Where(r => !r.IsAbstract);

            //foreach (var type in consumerTypes)
            //{
                //var c = container.Resolve(typeof(MessageConsumer<>).MakeGenericType(type));
                //e.Consumer(c.GetType(), (t) => container.Resolve(typeof(MessageConsumer<>).MakeGenericType(type)));

                //e.Consumer(c.GetType(), (t) => container.Resolve(typeof(MessageConsumer<>).MakeGenericType(t)));

                //e.Consumer(consumer,  (t) => { return container.Resolve(t); });
            //}
            

            //builder.var consumerTypes = builder.ReAssembly.GetExecutingAssemblyComponentRegistry.Registrations
            //            .Where(r => typeof(ServiceData).IsAssignableFrom(r.Activator.LimitType))
            //            .Select(r => r.Activator.LimitType)
            //            .Where(r => !r.IsAbstract);

            //foreach (var type in consumerTypes)
            //{
            //    var c = container.Resolve(typeof(MessageConsumer<>).MakeGenericType(type));
            //    e.Consumer(c.GetType(), (t) => container.Resolve(typeof(MessageConsumer<>).MakeGenericType(type)));

            //    //e.Consumer(c.GetType(), (t) => container.Resolve(typeof(MessageConsumer<>).MakeGenericType(t)));

            //    //e.Consumer(consumer,  (t) => { return container.Resolve(t); });
            //}

            //builder.Register<Func<object, IConsumer>>(context => theObject =>
            //{
            //    var concreteType =
            //        typeof(IConsumer<>).MakeGenericType(theObject.GetType());
            //    return (IFlattener)context.Resolve(concreteType,
            //        new PositionalParameter(0, theObject));
            //});

            //var b = builder.Build();

            //IConsumer<ServiceData> a;
            //b.TryResolve<IConsumer<ServiceData>>(out a);

            //IConsumer<IServiceData> i;
            //b.TryResolve<IConsumer<IServiceData>>(out i);

            //builder.RegisterAssemblyTypes(typeof(ServiceBusModule).Assembly)
            //    .Where(t =>
            //    {
            //        var a = typeof(IConsumer).IsAssignableFrom(t);
            //        return a;
            //    })
            //    .AsSelf();
        }
    }
}