namespace Framework.Queue
{
    public class QueueMessage<T> : IQueueMessage<T> where T : class
    {
        public T Body { get; }

        public IQueueAction<T> Action { get; }
    }
}
