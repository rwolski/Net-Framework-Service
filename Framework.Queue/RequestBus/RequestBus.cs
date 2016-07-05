using MassTransit;
using System;
using System.Threading.Tasks;

namespace Framework.Queue
{
    internal class RequestBus<T> : IRequestBus<T>, IDisposable where T : class
    {
        readonly IBusControl _connection;
        readonly IRequestClient<IMessageRequest<T>, IMessageResponse<T>> _requestClient;

        readonly IServiceProviderSettings _settings;
        readonly string _queueName;

        #region Constructors

        public RequestBus(IServiceProviderSettings settings, string queueName)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            if (string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            settings.Prefix = "rabbitmq";

            _connection = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(settings.BuildUri(),  h => //{ });
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });
            });

            _requestClient = new MessageRequestClient<IMessageRequest<T>, IMessageResponse<T>>(_connection, new Uri("rabbitmq://localhost:5672/" + _queueName), TimeSpan.FromSeconds(30));

            _connection.Start();

            _queueName = queueName;
            _settings = settings;
        }

        #endregion

        public async Task<IMessageResponse<T>> Request(IMessageRequest<T> request = null)
        {
            if (request == null)
                request = new QueueMessageRequest<T>();

            return await _requestClient.Request(request);
        }

        #region IDispose

        ~RequestBus()
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