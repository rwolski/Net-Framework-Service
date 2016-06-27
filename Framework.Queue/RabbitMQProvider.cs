using RabbitMQ.Client;
using System;

namespace Core
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

        public IQueue GetQueue(string queueName/*, bool cached = false*/)
        {
            if (string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            return new RabbitMQQueue(_client, queueName);
        }
    }
}
