namespace Framework.ServiceBus
{
    public interface IServiceAction<T>
    {
        T Data { get; }

        void PerformAction();
    }
}