namespace Framework.Queue
{
    public interface IQueueProvider
    {
        IQueue<T> GetQueue<T>(string queueName) where T : class, IQueueMessage;

        IQueue<T> GetQueue<T>() where T : class, IQueueMessage;
    }
}
