using Autofac;
using MassTransit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public class ServiceBusModule : Autofac.Module
    {
        public interface ITestContract1 : IServiceContract
        {
            int Val { get; }
        }

        public class TestContract1 : ITestContract1
        {
            public int Val { get; set; }
        }

        public interface ITestContract2 : IServiceContract
        {
            string Val { get; }
        }

        public class TestContract2 : ITestContract2
        {
            public string Val { get; set; }
        }

        public class ServiceContractAction1 : ServiceContractAction<ITestContract1>
        {
            //public ITestContract1 Contract { get; protected set; }

            public ServiceContractAction1(ITestContract1 contract, ILifetimeScope scope)
                : base(contract)
            {
            }

            public override Task Action()
            {
                return Task.FromResult(0);
            }
        }

        public class ServiceContractAction2 : ServiceContractAction<ITestContract2>
        {
            //public ITestContract2 Contract { get; protected set; }

            public ServiceContractAction2(ITestContract2 contract, ILifetimeScope scope)
                : base(contract)
            {
            }

            public override Task Action()
            {
                return Task.FromResult(0);
            }
        }


        void RegisterType1(ContainerBuilder builder)
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
        }

        void RegisterType2(ContainerBuilder builder)
        {
            // Register the second test consumer
            //builder.RegisterType<MessageConsumer2<ITestContract>>();
            //builder.RegisterGeneric(typeof(MessageConsumer2<>)).AsSelf();

            // Register all message types derived from IServiceData
            var consumerTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                //.Where(p => typeof(IServiceContract).IsAssignableFrom(p) && p != typeof(IServiceContract));
                //.Where(p => typeof(IServiceContract).IsAssignableFrom(p) && !p.IsAbstract);
                .Where(t => t.BaseType == typeof(IServiceContract));

            //builder.RegisterType<TestContract>().As<ITestContract>();
            //builder.RegisterType<TestContract1>().As<ITestContract1>();
            //builder.RegisterType<MessageConsumer2<TestContract>>();
            //builder.RegisterType<MessageConsumer2<TestContract1>>();
            builder.RegisterType<MessageConsumer2<ITestContract1>>();
            builder.RegisterType<MessageConsumer2<ITestContract2>>();

            //foreach (var type in consumerTypes)
            //{
            //    //builder.RegisterType(type).AsImplementedInterfaces().InstancePerRequest();
            //    builder.RegisterType(typeof(MessageConsumer2<>).MakeGenericType(type));
            //}

            // Register all generic types of contract actions
            //builder.RegisterGeneric(typeof(ServiceContractAction<>)).As(typeof(IServiceContractAction<>));
            builder.RegisterType<ServiceContractAction1>().As<IServiceContractAction<ITestContract1>>();
            builder.RegisterType<ServiceContractAction2>().As<IServiceContractAction<ITestContract2>>();
            //builder.RegisterType<ServiceContractAction<TestContract1>>().As<IServiceContractAction<TestContract1>>();
            //builder.RegisterType<ServiceContractAction<ITestContract1>>().As<IServiceContractAction<ITestContract1>>();



            // Register the service contract action
            //builder.RegisterType<TestContract>().As<ITestContract>();
            //builder.RegisterAssemblyTypes(this.GetType().Assembly).Where(x => x.BaseType == typeof(IServiceContract)).



            // Casting the base class to upper class
            //builder.RegisterAssemblyTypes(typeof(IServiceContractAction<>).Assembly)
            //.Where(t => t.BaseType == typeof(ServiceContractAction<ITestContract>))
            //.As<ServiceContractAction<ITestContract>>();

            //builder.RegisterGeneric(typeof(ServiceContractAction<>)).As(typeof(IServiceContractAction<>));
            //builder.RegisterType<TestContractAction>().As<ServiceContractAction<ITestContract>>().InstancePerLifetimeScope();

        }

        protected override void Load(ContainerBuilder builder)
        {
            //RegisterType1(builder);

            RegisterType2(builder);

        }
    }
}