using Autofac;
using Framework.Core;
using MassTransit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    internal class ServiceBus : IServiceBus
    {
        readonly IBusControl _connection;

        readonly IServiceProviderSettings _settings;
        readonly string _queueName;

        #region Constructors

        public ServiceBus(ILifetimeScope container, IServiceProviderSettings settings, string queueName)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            if (string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            settings.Protocol = "rabbitmq";

            _connection = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var uri = settings.BuildUri();
                var host = cfg.Host(uri,  h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });

                cfg.ReceiveEndpoint(host, queueName, e =>
                {
                    e.LoadFrom(container);
                    //e.Consumer(factory);

                    //var consumerTypes = container.ComponentRegistry.Registrations
                    //    .Where(r => typeof(ServiceData).IsAssignableFrom(r.Activator.LimitType))
                    //    .Select(r => r.Activator.LimitType)
                    //    .Where(r => !r.IsAbstract);
                    
                    //foreach (var type in consumerTypes)
                    //{
                    //    var c = container.Resolve(typeof(MessageConsumer<>).MakeGenericType(type));
                    //    e.Consumer(c.GetType(), (t) => container.Resolve(typeof(MessageConsumer<>).MakeGenericType(type)));

                    //    //e.Consumer(c.GetType(), (t) => container.Resolve(typeof(MessageConsumer<>).MakeGenericType(t)));

                    //    //e.Consumer(consumer,  (t) => { return container.Resolve(t); });
                    //}
                });
            });

            _connection.Start();

            _queueName = queueName;
            _settings = settings;
        }

        #endregion

        #region Send

        public async virtual Task Send<TData>(TData message) where TData : class
        {
            var uri = _settings.BuildUri(_queueName + "/");
            var endpoint = await _connection.GetSendEndpoint(uri);
            await endpoint.Send<TData>(message);
        }

        public virtual Task Publish<TData>(TData message) where TData : class
        {
            return _connection.Publish<TData>(message);
        }

        #endregion

        #region IDispose

        ~ServiceBus()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (disposing)
            {
                _connection.Stop();
            }
        }

        #endregion
    }
}