using Autofac;
using Framework.Core;
using System;

namespace Framework.ServiceBus
{
    public class ServiceBusProvider : IServiceBusProvider
    {
        readonly IServiceProviderSettings _settings;
        readonly ILifetimeScope _scope;


        public ServiceBusProvider(ILifetimeScope scope, IServiceProviderSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            if (string.IsNullOrWhiteSpace(settings.Hostname))
                throw new ArgumentNullException("settings.Hostname");
            if (settings.Port <= 0)
                throw new ArgumentOutOfRangeException("settings.Port");

            _settings = settings;
            _scope = scope;
        }

        public IServiceBus GetBus(string queueName)
        {
            if (string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            return new ServiceBus(_scope, _settings, queueName);
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
