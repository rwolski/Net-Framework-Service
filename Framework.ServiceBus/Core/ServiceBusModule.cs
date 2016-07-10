using Autofac;
using MassTransit;
using System;
using System.Linq;
using System.Reflection;

namespace Framework.ServiceBus
{
    public class ServiceBusModule : Autofac.Module
    {
        Assembly[] _assemblies;

        public ServiceBusModule(params Assembly[] assemblies)
        {
            _assemblies = assemblies ?? AppDomain.CurrentDomain.GetAssemblies();
        }

        public ServiceBusModule()
        {
            _assemblies = AppDomain.CurrentDomain.GetAssemblies();
        }

        //void RegisterType1(ContainerBuilder builder)
        //{
        //    // Register the generic message consumer
        //    builder.RegisterGeneric(typeof(MessageConsumer<>)).AsSelf();

        //    // Register all message types derived from IServiceData
        //    var consumerTypes = AppDomain.CurrentDomain.GetAssemblies()
        //        .SelectMany(s => s.GetTypes())
        //        .Where(p => typeof(IServiceEvent).IsAssignableFrom(p) && !p.IsAbstract);

        //    foreach (var type in consumerTypes)
        //    {
        //        builder.RegisterType(type).AsImplementedInterfaces().InstancePerRequest();
        //        builder.RegisterType(typeof(MessageConsumer<>).MakeGenericType(type));
        //    }
        //}


        protected override void Load(ContainerBuilder builder)
        {
            var assemblyTypes = _assemblies
                .SelectMany(s => s.GetTypes());

            // Get all the contract types derives from the contract interface
            var contractTypes = assemblyTypes
                .Where(p => typeof(IMessageContract).IsAssignableFrom(p) && p.IsAbstract && p != typeof(IMessageContract));

            foreach (var type in contractTypes)
            {
                // Register a consumer for the contract itself
                builder.RegisterType(typeof(ContractMessageConsumer<>).MakeGenericType(type)).InstancePerLifetimeScope();

                var genericActionType = typeof(IMessageAction<>).MakeGenericType(type);
                var actionType = assemblyTypes
                    .Where(r => genericActionType.IsAssignableFrom(r))
                    .FirstOrDefault();

                if (actionType != null)
                    builder.RegisterType(actionType).As(genericActionType);
                //else
                //throw new InvalidOperationException("Message contract provided without consuming action");
            }

            // Consumers
            //builder.RegisterType<ContractMessageConsumer<ITestContract1>>();
            //builder.RegisterType<ContractMessageConsumer<ITestContract2>>();

            // Contract actions
            //builder.RegisterType<ServiceContractAction1>().As<IMessageAction<ITestContract1>>();
            //builder.RegisterType<ServiceContractAction2>().As<IMessageAction<ITestContract1>>();



            var genericRequests = contractTypes.Select(x => typeof(IMessageRequest<>).MakeGenericType(x));
            var requestTypes = genericRequests
                .SelectMany(r => assemblyTypes.Where(t => r.IsAssignableFrom(t) && t.IsAbstract && t != typeof(IMessageRequest<>)));

            foreach (var type in requestTypes)
            {
                // Register a consumer for the contract itself
                builder.RegisterType(typeof(RequestMessageConsumer<>).MakeGenericType(type)).InstancePerLifetimeScope();

                var genericActionType = typeof(IMessageRequestHandler<>).MakeGenericType(type);
                var actionType = assemblyTypes
                    .Where(r => genericActionType.IsAssignableFrom(r))
                    .FirstOrDefault();

                if (actionType != null)
                    builder.RegisterType(actionType).As(genericActionType);
                //else
                //throw new InvalidOperationException("Message request provided without consuming response");
            }

            // OR register the explicity types within the API level

            // Consumers
            //builder.RegisterType<RequestMessageConsumer<ITestRequest>>().InstancePerLifetimeScope();
            //builder.RegisterType<RequestMessageConsumer<ITestRequest1>>().InstancePerLifetimeScope();

            // Contract actions
            //builder.RegisterType<TestRequestHandler>().As<IMessageRequestHandler<ITestRequest>>();
            //builder.RegisterType<TestRequestHandler>().As<IMessageRequestHandler<ITestRequest1>>();

            //RegisterType1(builder);

        }
    }
}