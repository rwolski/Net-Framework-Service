using MassTransit;
using System;
using System.Text;

namespace Framework.Queue
{
    internal class MassTransitQueue<T> : IQueue<T>, IDisposable
    {
        readonly IBusControl _connection;
        //readonly BusHandle _busHandle;
        readonly IServiceProviderSettings _settings;
        protected readonly string _queueName;

        #region Constructors

        public MassTransitQueue(IServiceProviderSettings settings, string queueName)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            if (string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            _connection = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(settings.BuildUri(), h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });

                cfg.UseJsonSerializer();
                cfg.ReceiveEndpoint(host, queueName, e =>
                    e.Consumer<MassTransitMessageConsumer<T>>());
            });

            _connection.Start();

            _queueName = queueName;
            _settings = settings;
        }

        #endregion

        #region Send

        public async virtual void Send(T message)
        {
            var endpoint = _connection.GetSendEndpoint(new Uri("rabbitmq://localhost/" + _queueName)).Result;
            await endpoint.Send(message, message.GetType());
        }

        public async virtual void Publish(T message)
        {
            await _connection.Publish(message, message.GetType());
        }

        #endregion

        #region Receive

        public virtual T Receive()
        {
            //_connection.ConnectHandler<T>()
            Func<MassTransitMessageConsumer<T>> fn = () =>
            {
                return new MassTransitMessageConsumer<T>();
            };
            var handle = _connection.ConnectConsumer(fn);

            return default(T);
        }

        public virtual void Consume()
        {

        }

        #endregion

        #region IDispose

        ~MassTransitQueue()
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
            //_channel.Dispose();
            //_connection.Dispose();
        }

        #endregion
    }
}