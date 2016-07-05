using RabbitMQ.Client;
using System;
using System.Linq;

namespace Framework.Queue
{
    public sealed class RabbitMQProvider : IQueueProvider
    {
        readonly ConnectionFactory _client;


        public RabbitMQProvider(string hostname = "localhost", int port = 5672)
        {
            if (string.IsNullOrWhiteSpace(hostname))
                throw new ArgumentNullException("hostname");
            if (port <= 0)
                throw new ArgumentOutOfRangeException("port");

            _client = new ConnectionFactory()
            {
                HostName = hostname,
                Port = port
            };
        }

        public IQueue<T> GetQueue<T>(string queueName) where T : class, IQueueMessage
        {
            if (string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            return new RabbitMQQueue<T>(_client, queueName);
        }

        public IQueue<T> GetQueue<T>() where T : class, IQueueMessage
        {
            var queueName = GetQueueFromType<T>();

            return new RabbitMQQueue<T>(_client, queueName);
        }

        private string GetQueueFromType<T>()
        {
            var attr = typeof(T).GetCustomAttributes(typeof(QueuedEntityAttribute), false).FirstOrDefault() as QueuedEntityAttribute;
            if (attr == null || string.IsNullOrWhiteSpace(attr.EntityName))
                return typeof(T).Name;

            return attr.EntityName;
        }
    }
}
