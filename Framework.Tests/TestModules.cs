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
            //builder.RegisterType<MessageConsumer<ServiceBusTests.TestData1>>().InstancePerLifetimeScope().AsSelf();
            //builder.RegisterType<MessageConsumer<ServiceBusTests.TestData2>>().InstancePerLifetimeScope().AsSelf();
            builder.RegisterModule<ServiceBusModule>();

            builder.Register(c => new RedisProvider(AppSettings.RedisHostname, AppSettings.RedisPort)).As<ICacheProvider>().SingleInstance();
            builder.Register(c => new RabbitMQProvider(AppSettings.RabbitMQHostname, AppSettings.RabbitMQPort)).As<ISimpleQueueProvider>().SingleInstance();
            builder.Register(c => new ServiceBusProvider(c.Resolve<ILifetimeScope>(), AppSettings.RabbitMQHostname, (UInt16)AppSettings.RabbitMQPort)).As<IServiceBusProvider>().SingleInstance();
            builder.Register(c => new MongoDatabaseProvider(AppSettings.MongoDbHostname, AppSettings.MongoDbPort)).As<IDatabaseProvider>().SingleInstance();
        }
    }
}