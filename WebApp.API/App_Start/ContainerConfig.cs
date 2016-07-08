using Autofac;
using Autofac.Integration.WebApi;
using Framework.Cache;
using Framework.Core;
using Framework.Data;
using Framework.Queue;
using Framework.ServiceBus;
using Framework.WebSockets;
using System;
using System.Reflection;
using System.Web.Http;

namespace WebApp.API
{
    /// <summary>
    /// Autofac container config
    /// </summary>
    public class ContainerConfig
    {
        /// <summary>
        /// Builds this container instance.
        /// </summary>
        /// <returns></returns>
        public static ContainerBuilder RegisterModules(ContainerBuilder builder = null)
        {
            if (builder == null)
                builder = new ContainerBuilder();

            // Controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Mongo
            var mongoProvider = new MongoDatabaseProvider(AppSettings.MongoDbHostname, AppSettings.MongoDbPort, AppSettings.MongoDbDatabase);
            builder.Register(c => mongoProvider).As<IDatabaseProvider>().SingleInstance();
            builder.Register(c => mongoProvider.GetDatabase()).As<IDatabaseConnection>().InstancePerLifetimeScope();

            // Redis
            builder.Register(c => new RedisProvider(AppSettings.RedisHostname, AppSettings.RedisPort)).As<ICacheProvider>().SingleInstance();
            //builder.Register(c => c.Resolve<ICacheProvider>().GetStore(AppSettings.RedisDatabaseIndex)).As<ICacheStore>().InstancePerRequest();
            builder.Register(c => c.Resolve<ICacheProvider>().GetStore(AppSettings.RedisDatabaseIndex)).As<ICacheStore>().InstancePerLifetimeScope();

            // Rabbit
            builder.Register(c => new RabbitMQProvider(AppSettings.RabbitMQHostname, AppSettings.RabbitMQPort)).As<ISimpleQueueProvider>().SingleInstance();

            // Mass transit & consumers
            var serviceBusSettings = new ServiceProviderSettings()
            {
                Hostname = AppSettings.RabbitMQHostname,
                Port = AppSettings.RabbitMQPort,
                Username = "guest",
                Password = "guest"
            };
            builder.RegisterModule(new ServiceBusModule());
            builder.Register(c => new ServiceBusProvider(c.Resolve<ILifetimeScope>(), serviceBusSettings))
                .As<IServiceBusProvider>().SingleInstance();
            builder.Register(c => new ServiceBus(c.Resolve<ILifetimeScope>(), serviceBusSettings, AppSettings.AppServiceBusName))
                .As<IServiceBus>().SingleInstance();

            // SignalR
            builder.RegisterType<SignalRProvider>().As<ISocketProvider>().SingleInstance();

            return builder;
        }

        /// <summary>
        /// Builds the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IContainer Build(ContainerBuilder builder)
        {
            var container = builder.Build();
            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container.BeginLifetimeScope());

            return container;
        }
    }
}