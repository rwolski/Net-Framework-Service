namespace Framework.Queue
{
    public class QueueMessage<T> : IQueueMessage
    {
        public virtual void PerformAction()
        {
            var i = 3;
        }
    }
}
