namespace Framework.Queue
{
    public interface IRequestBusProvider
    {
        IRequestBus<T> GetQueue<T>(string queueName) where T : class;

        IRequestBus<T> GetQueue<T>() where T : class;
    }
}
