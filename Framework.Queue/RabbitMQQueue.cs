using RabbitMQ.Client;
using System;
using System.Text;

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

        public virtual void Send(T message)
        {
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            var bytes = Encoding.UTF8.GetBytes(str);

            _channel.BasicPublish("", _queueName, null, bytes);
        }

        public virtual void Publish(T message)
        {
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            var bytes = Encoding.UTF8.GetBytes(str);

            // Should send to all 
            _channel.BasicPublish("", _queueName, null, bytes);
        }

        #endregion

        #region Receive

        public virtual T Receive()
        {
            var result = _channel.BasicGet(_queueName, true);
            if (result == null)
                return default(T);

            var jsonStr = Encoding.UTF8.GetString(result.Body);
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonStr);

            return obj;
        }

        public virtual void Consume()
        {

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
