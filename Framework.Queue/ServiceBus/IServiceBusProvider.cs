namespace Framework.Queue
{
    public interface IServiceBusProvider
    {
        IServiceBus<T> GetBus<T>(string queueName) where T : class, IQueueMessage;

        IServiceBus<T> GetBus<T>() where T : class, IQueueMessage;
    }
}
