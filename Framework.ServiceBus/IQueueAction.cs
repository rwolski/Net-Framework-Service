namespace Framework.ServiceBus
{
    public interface IQueueAction<T>
    {
        void PerformAction(T message);
    }
}