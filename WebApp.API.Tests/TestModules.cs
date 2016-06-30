using Autofac;
using Autofac.Integration.WebApi;
using Framework.Cache;
using Framework.Queue;
using System.Reflection;

namespace WebApp.API.Tests
{
    internal class TestModules : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
                builder = new ContainerBuilder();

            ContainerConfig.RegisterModules(builder);

            //builder.RegisterApiControllers(typeof(ContainerConfig).Assembly);

            //// Register the classes we need
            //builder.Register(c => new RedisProvider(AppSettings.RedisHostname, AppSettings.RedisPort)).As<ICacheProvider>().SingleInstance();
            //builder.Register(c => new RabbitMQProvider(AppSettings.RabbitMQHostname, AppSettings.RabbitMQPort)).As<IQueueProvider>().SingleInstance();
            //builder.Register(c => new MongoDatabaseProvider(AppSettings.MongoDbHostname, AppSettings.MongoDbPort, AppSettings.MongoDbDatabase))
            //    .As<IDatabaseProvider>().SingleInstance();

            //var container = builder.Build();
            //var config = GlobalConfiguration.Configuration;
            //var localhost = new HttpConfiguration();
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container.BeginLifetimeScope());
            //localhost.DependencyResolver = new AutofacWebApiDependencyResolver(container.BeginLifetimeScope());

            //return container;
        }
    }
}
