namespace Framework.Queue
{
    public interface ISimpleQueueProvider
    {
        ISimpleQueue<T> GetQueue<T>(string queueName);

        ISimpleQueue<T> GetQueue<T>();
    }
}
