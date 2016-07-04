using Autofac;
using Framework.Cache;
using Framework.Data;
using Framework.Queue;
using System;

namespace Framework.Tests
{
    internal class TestModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new RedisProvider(AppSettings.RedisHostname, AppSettings.RedisPort)).As<ICacheProvider>().SingleInstance();
            builder.Register(c => new RabbitMQProvider(AppSettings.RabbitMQHostname, AppSettings.RabbitMQPort)).As<IQueueProvider>().Keyed<IQueueProvider>(QueueProviderType.RabbitMQ).SingleInstance();
            builder.Register(c => new MassTransitProvider(AppSettings.RabbitMQHostname, (UInt16)AppSettings.RabbitMQPort)).As<IQueueProvider>().Keyed<IQueueProvider>(QueueProviderType.MassTransit).SingleInstance();
            builder.Register(c => new MongoDatabaseProvider(AppSettings.MongoDbHostname, AppSettings.MongoDbPort)).As<IDatabaseProvider>().SingleInstance();
        }
    }
}