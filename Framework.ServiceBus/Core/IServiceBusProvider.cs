namespace Framework.ServiceBus
{
    public interface IServiceBusProvider
    {
        IServiceBus GetBus(string queueName);

        //IServiceBus<T> GetBus<T>() where T : class;
    }
}
