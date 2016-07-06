namespace Framework.ServiceBus
{
    public class QueueAction<T> : IQueueAction<T>
    {
        public virtual void PerformAction(T message)
        {
            var i = 3;
        }
    }
}
