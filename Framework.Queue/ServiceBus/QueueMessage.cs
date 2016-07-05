namespace Framework.Queue
{
    public class QueueMessage<T> : IQueueMessage<T> where T : IQueueAction
    {
        T Action { get; }

        public virtual void PerformAction()
        {
            var i = 3;
        }
    }
}
