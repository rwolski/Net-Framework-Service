using Autofac;
using Framework.Cache;
using Framework.Data;
using Framework.Queue;
using Framework.ServiceBus;
using System;
using System.Linq;

namespace Framework.Tests
{
    internal class TestModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<ServiceBusTests.TestData>().As<IServiceData>().InstancePerLifetimeScope();
            builder.RegisterType<MessageConsumer<ServiceBusTests.TestData>>();
            //builder.RegisterModule<ServiceBusModule>();

            //var types = AppDomain.CurrentDomain.GetAssemblies()
            //    .SelectMany(s => s.GetTypes())
            //    .Where(p => typeof(IServiceData).IsAssignableFrom(p));
            //builder.RegisterGeneric(typeof(MessageConsumer<>))
            //    .As
            //foreach (var type in types)
            //{
            //    builder.RegisterType<<type>>();
            //}

            //builder.RegisterType<ServiceBusTests.TestAction>().As <IServiceAction<IServiceData>>();
            builder.Register(c => new RedisProvider(AppSettings.RedisHostname, AppSettings.RedisPort)).As<ICacheProvider>().SingleInstance();
            builder.Register(c => new RabbitMQProvider(AppSettings.RabbitMQHostname, AppSettings.RabbitMQPort)).As<ISimpleQueueProvider>().SingleInstance();
            builder.Register(c => new ServiceBusProvider(c.Resolve<ILifetimeScope>(), AppSettings.RabbitMQHostname, (UInt16)AppSettings.RabbitMQPort)).As<IServiceBusProvider>().SingleInstance();
            builder.Register(c => new MongoDatabaseProvider(AppSettings.MongoDbHostname, AppSettings.MongoDbPort)).As<IDatabaseProvider>().SingleInstance();
        }
    }
}