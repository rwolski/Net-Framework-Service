namespace Framework.Queue
{
    public interface IQueueMessage<T> where T : class
    {
        T Body { get; }

        IQueueAction<T> Action { get; }
    }
}