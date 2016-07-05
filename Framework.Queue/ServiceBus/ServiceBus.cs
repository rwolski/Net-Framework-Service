using Autofac;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Framework.Queue
{
    internal class ServiceBus<T> : IServiceBus<T>, IDisposable, IComponentContext where T : class, IQueueMessage
    {
        readonly IBusControl _connection;

        readonly IServiceProviderSettings _settings;
        readonly string _queueName;

        #region Constructors

        public ServiceBus(IContainer container, IServiceProviderSettings settings, string queueName)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            if (string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            settings.Prefix = "rabbitmq";

            _connection = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(settings.BuildUri(),  h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });

                cfg.ReceiveEndpoint(host, queueName, e =>
                {
                    e.LoadFrom(container);
                    //e.Consumer(() => container.Resolve<IConsumerFactory<T>>());
                });
            });

            _connection.Start();

            _queueName = queueName;
            _settings = settings;
        }

        #endregion

        #region Send

        public async virtual Task Send(T message)
        {
            var endpoint = await _connection.GetSendEndpoint(new Uri("rabbitmq://localhost:5672/" + _queueName));
            await endpoint.Send<T>(message);
        }

        public virtual Task Publish(T message)
        {
            return _connection.Publish<T>(message);
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