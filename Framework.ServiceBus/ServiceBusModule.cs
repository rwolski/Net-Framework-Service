using Autofac;
using MassTransit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public class ServiceBusModule : Autofac.Module
    {
        public class TestContract : IServiceContract
        {
            public int Val { get; set; }
        }

        public class TestContractAction : IServiceContractAction<TestContract>
        {
            public TestContract Contract { get; protected set; }

            public TestContractAction(TestContract contract, ILifetimeScope scope)
            {
                Contract = contract;
            }

            public Task Action()
            {
                return Task.FromResult(0);
            }
        }

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
                builder.RegisterType(type).AsImplementedInterfaces().InstancePerRequest();
                builder.RegisterType(typeof(MessageConsumer<>).MakeGenericType(type));
            }

            // Register the second test consumer
            builder.RegisterType<MessageConsumer2<IServiceContract>>();

            // Register the service contract action
            builder.RegisterType<TestContract>().As<IServiceContract>();
            builder.RegisterGeneric(typeof(ServiceContractAction<>)).As(typeof(IServiceContractAction<>));
            //builder.RegisterType<TestContractAction>().As<ServiceContractAction<IServiceContract>>().As<IServiceContractAction<IServiceContract>>();
            //builder.RegisterType<TestContractAction>().As<ServiceContractAction<TestContract>>().As<IServiceContractAction<TestContract>>();

        }
    }
}