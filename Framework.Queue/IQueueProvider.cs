namespace Framework.Queue
{
    public interface IQueueProvider
    {
        IQueue<T> GetQueue<T>(string queueName = null);
    }
}
