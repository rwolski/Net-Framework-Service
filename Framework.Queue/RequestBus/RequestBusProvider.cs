using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Queue
{
    public class RequestBusProvider : IRequestBusProvider
    {
        readonly IServiceProviderSettings _settings;


        public RequestBusProvider(string hostname = "localhost", UInt16 port = 5672, string username = "guest", string password = "guest")
        {
            if (string.IsNullOrWhiteSpace(hostname))
                throw new ArgumentNullException("hostname");
            if (port <= 0)
                throw new ArgumentOutOfRangeException("port");

            _settings = new ServiceProviderSettings(hostname, port);
        }

        public IRequestBus<T> GetQueue<T>(string queueName) where T : class
        {
            if (string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            return new RequestBus<T>(_settings, queueName);
        }

        public IRequestBus<T> GetQueue<T>() where T : class
        {
            var queueName = GetQueueFromType<T>();

            return new RequestBus<T>(_settings, queueName);
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
