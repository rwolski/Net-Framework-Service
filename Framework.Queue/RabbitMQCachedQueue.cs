using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Core
{
    internal class RabbitMQCachedQueue : RabbitMQQueue, ICached
    {
        readonly ICacheProvider _cacheProvider;

        #region Constructors

        public RabbitMQCachedQueue(ConnectionFactory factory, string queueName, ICacheProvider cacheProvider)
            : base(factory, queueName)
        {
            _cacheProvider = cacheProvider;
        }

        #endregion

        #region Send

        public override void Send(object message)
        {
            base.Send(message);
        }

        #endregion

        #region Receive

        public override T Receive<T>()
        {
            //var obj = CheckCache<T>();

            //if (obj != null)
              //  return obj;

            return base.Receive<T>();
        }

        #endregion
    }
}
