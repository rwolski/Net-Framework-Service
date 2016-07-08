using Autofac;
using Framework.Core;
using MassTransit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public class ServiceBus : IServiceBus
    {
        readonly IBusControl _connection;

        readonly IServiceProviderSettings _settings;
        readonly string _queueName;

        const int _defaultTimeoutSeconds = 30;

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
                });
            });

            _connection.Start();

            _queueName = queueName;
            _settings = settings;
        }

        #endregion

        public async virtual Task Send<TData>(TData message, CancellationToken ct = default(CancellationToken))
            where TData : class
        {
            var uri = _settings.BuildUri(_queueName + "/");
            var endpoint = await _connection.GetSendEndpoint(uri);
            await endpoint.Send<TData>(message);
        }

        public virtual Task Publish<TData>(object message, CancellationToken ct = default(CancellationToken))
            where TData : class
        {
            return _connection.Publish<TData>(message);
        }

        public virtual Task Publish<TData>(TData message, CancellationToken ct = default(CancellationToken))
            where TData : class
        {
            return _connection.Publish(message);
        }

        public virtual Task<TData> Request<TReq, TData>(TReq request, CancellationToken ct = default(CancellationToken))
            where TReq : class, IMessageRequest
            where TData : class
        {
            var requestHandle = _connection.CreatePublishRequestClient<TReq, TData>(TimeSpan.FromSeconds(_defaultTimeoutSeconds));
            return requestHandle.Request(request, ct);
        }

        public virtual Task<TData> Request<TReq, TData>(TReq request, string destination, CancellationToken ct = default(CancellationToken))
            where TReq : class, IMessageRequest
            where TData : class
        {
            var requestHandle = _connection.CreateRequestClient<TReq, TData>(_settings.BuildUri(_queueName + "/"),
                TimeSpan.FromSeconds(_defaultTimeoutSeconds));
            return requestHandle.Request(request, ct);
        }

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