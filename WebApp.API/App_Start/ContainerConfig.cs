using Autofac;
using Autofac.Integration.WebApi;
using Framework.Cache;
using Framework.Data;
using Framework.Queue;
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

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register the classes we need
            var mongoProvider = new MongoDatabaseProvider(AppSettings.MongoDbHostname, AppSettings.MongoDbPort, AppSettings.MongoDbDatabase);

            builder.Register(c => new RedisProvider(AppSettings.RedisHostname, AppSettings.RedisPort)).As<ICacheProvider>().SingleInstance();
            builder.Register(c => new RabbitMQProvider(AppSettings.RabbitMQHostname, AppSettings.RabbitMQPort)).As<ISimpleQueueProvider>().SingleInstance();
            builder.Register(c => mongoProvider).As<IDatabaseProvider>().SingleInstance();
            builder.Register(c => mongoProvider.GetDatabase()).As<IDatabaseConnection>().InstancePerRequest();

            //builder.RegisterModule(new QueueModule());

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