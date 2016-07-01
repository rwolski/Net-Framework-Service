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
            builder.Register(c => new RedisProvider(AppSettings.RedisHostname, AppSettings.RedisPort)).As<ICacheProvider>().SingleInstance();
            builder.Register(c => new RabbitMQProvider(AppSettings.RabbitMQHostname, AppSettings.RabbitMQPort)).As<IQueueProvider>().SingleInstance();
            builder.Register(c => new MongoDatabaseProvider(AppSettings.MongoDbHostname, AppSettings.MongoDbPort, AppSettings.MongoDbDatabase))
                .As<IDatabaseProvider>().SingleInstance();

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
            //var localhost = new HttpConfiguration();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container.BeginLifetimeScope());
            //localhost.DependencyResolver = new AutofacWebApiDependencyResolver(container.BeginLifetimeScope());

            return container;
        }
    }
}