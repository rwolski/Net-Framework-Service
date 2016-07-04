namespace Framework.Queue
{
    public interface IMessageResponse<T>
    {
        T Body { get; }
    }

    public class QueueMessageResponse<T> : IMessageResponse<T>
    {
        public T Body { get; set; }

    }
}