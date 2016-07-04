namespace Framework.Queue
{
    public interface IMessageRequest<T>
    {

    }

    public class QueueMessageRequest<T> : IMessageRequest<T>
    {
    }
}