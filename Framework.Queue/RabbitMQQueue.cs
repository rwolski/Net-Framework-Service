using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Queue
{
    internal class RabbitMQQueue<T> : IQueue<T>
    {
        readonly IConnection _connection;
        readonly IModel _channel;
        protected readonly string _queueName;

        #region Constructors

        public RabbitMQQueue(ConnectionFactory factory, string queueName)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");
            if (string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _queueName = queueName;

            _channel.QueueDeclare(queueName, false, false, false, null);

            //_consumer = new EventingBasicConsumer(_channel);
            //_consumer.Received += new EventHandler<BasicDeliverEventArgs>(handler.Target, );
        }

        #endregion

        #region Send

        public virtual Task Send(T message)
        {
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            var bytes = Encoding.UTF8.GetBytes(str);

            _channel.BasicPublish("", _queueName, null, bytes);

            return Task.FromResult(true);
        }

        public virtual Task Publish(T message)
        {
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            var bytes = Encoding.UTF8.GetBytes(str);

            // Should send to all
            _channel.BasicPublish("", _queueName, null, bytes);

            return Task.FromResult(true);
        }

        #endregion

        #region Receive

        public virtual Task<T> Receive()
        {
            var result = _channel.BasicGet(_queueName, true);
            if (result == null)
                return Task.FromResult(default(T));

            var jsonStr = Encoding.UTF8.GetString(result.Body);
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonStr);

            return Task.FromResult(obj);
        }

        public virtual Task<T> Consume()
        {
            var result = _channel.BasicGet(_queueName, false);
            if (result == null)
                return Task.FromResult(default(T));

            var jsonStr = Encoding.UTF8.GetString(result.Body);
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonStr);

            return Task.FromResult(obj);
        }

        public virtual Task<IMessageResponse<T>> Request(IMessageRequest<T> request)
        {
            return null;
        }

        #endregion

        #region IDispose

        ~RabbitMQQueue()
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
            _channel.Dispose();
            _connection.Dispose();
        }

        #endregion
    }
}
