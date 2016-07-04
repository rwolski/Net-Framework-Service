namespace Framework.Queue
{
    public interface IQueueProvider
    {
        IQueue<T> GetQueue<T>(string queueName) where T : class;

        IQueue<T> GetQueue<T>() where T : class;
    }
}
