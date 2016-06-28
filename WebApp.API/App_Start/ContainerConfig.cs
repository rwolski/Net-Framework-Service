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
        public static IContainer RegisterModules()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //builder.RegisterModule(Core.)

            // Register the classes we need
            builder.Register(c => new RedisProvider(AppSettings.RedisHostname, AppSettings.RedisPort)).As<ICacheProvider>().SingleInstance();
            builder.Register(c => new RabbitMQProvider(AppSettings.RabbitMQHostname, AppSettings.RabbitMQPort)).As<IQueueProvider>().SingleInstance();
            builder.Register(c => new MongoDatabaseProvider(AppSettings.MongoDbHostname, AppSettings.MongoDbPort)).As<IDatabaseProvider>().SingleInstance();

            var container = builder.Build();
            var config = GlobalConfiguration.Configuration;
            //var localhost = new HttpSelfHostConfiguration("http://localhost:80");
            var localhost = new HttpConfiguration();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container.BeginLifetimeScope());
            localhost.DependencyResolver = new AutofacWebApiDependencyResolver(container.BeginLifetimeScope());

            return container;
        }
    }
}