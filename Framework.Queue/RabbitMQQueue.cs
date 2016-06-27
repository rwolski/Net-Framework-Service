using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Core
{
    internal class RabbitMQQueue : IQueue
    {
        readonly IConnection _connection;
        readonly IModel _channel;
        protected readonly string _queueName;
        //EventingBasicConsumer _consumer;

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
        }

        #endregion

        #region Send

        //public void AddConsumer(EventHandler<BasicDeliverEventArgs> handler)
        //{
        //    if (_consumer == null)
        //        _consumer = new EventingBasicConsumer(_channel);

        //    //_consumer.Received += new EventHandler<BasicDeliverEventArgs>(handler.Target, );
        //    //_consumer.
        //}

        public virtual void Send(object message)
        {
            _channel.QueueDeclare(_queueName, false, false, false, null);

            var str = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            var bytes = Encoding.UTF8.GetBytes(str);

            _channel.BasicPublish("", _queueName, null, bytes);
        }

        #endregion

        #region Receive

        public virtual T Receive<T>()
        {
            var result = _channel.BasicGet(_queueName, true);
            if (result == null)
                return default(T);

            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(result.Body));

            return obj;
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



    public class QueueUpdatedArgs : EventArgs
    {
    }
}
