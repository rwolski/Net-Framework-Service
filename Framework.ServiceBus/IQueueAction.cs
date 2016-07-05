namespace Framework.Queue
{
    public interface IQueueAction<T>
    {
        void PerformAction(T message);
    }
}