using Autofac;
using Framework.Cache;
using Framework.Core;
using Framework.Data;
using Framework.Queue;
using Framework.ServiceBus;

namespace Framework.Tests
{
    internal class TestModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<ServiceBusModule>();

            var serviceBusSettings = new ServiceProviderSettings()
            {
                Hostname = AppSettings.RabbitMQHostname,
                Port = AppSettings.RabbitMQPort,
                Username = "guest",
                Password = "guest"
            };
            builder.Register(c => new ServiceBusProvider(c.Resolve<ILifetimeScope>(), serviceBusSettings)).As<IServiceBusProvider>().SingleInstance();
            builder.Register(c => new ServiceBus.ServiceBus(c.Resolve<ILifetimeScope>(), serviceBusSettings, AppSettings.AppServiceBusName))
                .As<IServiceBus>().SingleInstance();

            builder.Register(c => new RedisProvider(AppSettings.RedisHostname, AppSettings.RedisPort)).As<ICacheProvider>().SingleInstance();
            builder.Register(c => new RabbitMQProvider(AppSettings.RabbitMQHostname, AppSettings.RabbitMQPort)).As<ISimpleQueueProvider>().SingleInstance();
            builder.Register(c => new MongoDatabaseProvider(AppSettings.MongoDbHostname, AppSettings.MongoDbPort)).As<IDatabaseProvider>().SingleInstance();
        }
    }
}