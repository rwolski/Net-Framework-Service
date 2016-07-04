using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Queue
{
    internal class MassTransitQueue<T> : IQueue<T>, IDisposable where T : class
    {
        readonly IBusControl _connection;
        readonly IRequestClient<IMessageRequest<T>, IMessageResponse<T>> _requestClient;

        readonly IServiceProviderSettings _settings;
        readonly string _queueName;

        #region Constructors

        public MassTransitQueue(IServiceProviderSettings settings, string queueName)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            if (string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            settings.Prefix = "rabbitmq";

            _connection = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(settings.BuildUri(), h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });

                cfg.UseJsonSerializer();
                cfg.AutoDelete = false;

                //cfg.Durable = true;

                //cfg.ReceiveEndpoint(host, queueName, e => e.Consumer(typeof(IQueueMessage), type => new MassTransitMessage<T>()));

                cfg.ReceiveEndpoint(host, queueName, e =>
                {
                    e.Durable = false;
                    e.Consumer<MassTransitMessageConsumer>();
                });
            });

            //_requestClient = new MessageRequestClient<IMessageRequest<T>, IMessageResponse<T>>(_connection, settings.BuildUri(), TimeSpan.FromSeconds(30));

            _connection.Start();

            _queueName = queueName;
            _settings = settings;
        }

        #endregion

        #region Send

        public async virtual Task Send(T message)
        {
            var endpoint = await _connection.GetSendEndpoint(new Uri("rabbitmq://localhost/" + _queueName));
            await endpoint.Send(message, message.GetType());
        }

        public virtual Task Publish(T message)
        {
            return _connection.Publish<T>(message);
        }

        public async Task<IMessageResponse<T>> Request(IMessageRequest<T> request = null)
        {
            if (request == null)
                request = new QueueMessageRequest<T>();
            return await _requestClient.Request(request);
        }

        #endregion

        #region Receive

        public Task<T> Receive()
        {
            //_connection.ConnectHandler<T>()
            //Func<MassTransitMessageConsumer<T>> fn = () =>
            //{
            //    return new MassTransitMessageConsumer<T>();
            //};
            //var handle = _connection.ConnectConsumer(fn);

            return Task.FromResult(default(T));
        }

        public Task<T> Consume()
        {
            return Task.FromResult(default(T));
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
        }

        #endregion
    }
}