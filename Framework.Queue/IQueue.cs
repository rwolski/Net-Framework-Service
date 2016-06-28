using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Queue
{
    public interface IQueue : IDisposable
    {
        //void AddConsumer(EventHandler<BasicDeliverEventArgs> handler);

        void Send(object message);

        T Receive<T>();
    }
}
