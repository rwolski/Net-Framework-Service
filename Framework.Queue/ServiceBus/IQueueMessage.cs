namespace Framework.Queue
{
    public interface IQueueMessage<T> where T : IQueueAction
    {
        IQueueAction Action { get; }
    }
}