using Autofac;

namespace Framework.ServiceBus
{
    public class QueueMessage<T> : IQueueMessage<T> where T : class
    {
        public QueueMessage(ILifetimeScope scope)
        {
            var i = 3;
        }

        public T Body { get; }

        public IQueueAction<T> Action { get; }
    }
}
