using Autofac;
using Framework.Core;
using System;

namespace Framework.Queue
{
    public class ServiceBusProvider : IServiceBusProvider
    {
        readonly IServiceProviderSettings _settings;
        readonly ILifetimeScope _scope;


        public ServiceBusProvider(ILifetimeScope scope, string hostname = "localhost", UInt16 port = 5672, string username = "guest", string password = "guest")
        {
            if (string.IsNullOrWhiteSpace(hostname))
                throw new ArgumentNullException("hostname");
            if (port <= 0)
                throw new ArgumentOutOfRangeException("port");

            _settings = new ServiceProviderSettings(hostname, port);
            _scope = scope;
        }

        public IServiceBus<T> GetBus<T>(string queueName) where T : class, IQueueMessage<T>
        {
            if (string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            return new ServiceBus<T>(_scope, _settings, queueName);
        }

        //public IServiceBus<T> GetQueue<T>() where T : class
        //{
        //    var queueName = GetQueueFromType<T>();

        //    return new ServiceBus<T>(_settings, queueName);
        //}

        //private string GetQueueFromType<T>()
        //{
        //    var attr = typeof(T).GetCustomAttributes(typeof(QueuedEntityAttribute), false).FirstOrDefault() as QueuedEntityAttribute;
        //    if (attr == null || string.IsNullOrWhiteSpace(attr.EntityName))
        //        return typeof(T).Name;

        //    return attr.EntityName;
        //}
    }
}
