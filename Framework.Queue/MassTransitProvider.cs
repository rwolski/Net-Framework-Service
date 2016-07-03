using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Queue
{
    public class MassTransitProvider : IQueueProvider//, IDisposable
    {
        //readonly IBusControl _client;
        readonly IServiceProviderSettings _settings;
        //const string _uriFormat = "{0}:{1}";
        //const string _username = "guest";
        //const string _password = "guest";


        public MassTransitProvider(string hostname = "localhost", UInt16 port = 5672, string username = "guest", string password = "guest")
        {
            if (string.IsNullOrWhiteSpace(hostname))
                throw new ArgumentNullException("hostname");
            if (port <= 0)
                throw new ArgumentOutOfRangeException("port");

            _settings = new ServiceProviderSettings(hostname, port);
        }

        public IQueue<T> GetQueue<T>(string queueName = null)
        {
            if (string.IsNullOrWhiteSpace(queueName))
                queueName = GetQueueFromType<T>();

            return new MassTransitQueue<T>(_settings, queueName);
        }

        private string GetQueueFromType<T>()
        {
            var attr = typeof(T).GetCustomAttributes(typeof(QueuedEntityAttribute), false).FirstOrDefault() as QueuedEntityAttribute;
            if (attr == null || string.IsNullOrWhiteSpace(attr.EntityName))
                return typeof(T).Name;

            return attr.EntityName;
        }

        //#region Disposable

        //~MassTransitProvider()
        //{
        //    Dispose(false);
        //    GC.SuppressFinalize(this);
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //}

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _client.Stop();
        //    }
        //}

        //#endregion
    }
}
