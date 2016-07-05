using MassTransit;
using System.Threading.Tasks;

namespace Framework.Queue
{
    public class MessageConsumer<T> : IConsumer<T> where T : class, IQueueMessage<T>
    {
        T _message { get; set; }

        public MessageConsumer()
        {
        }

        public Task Consume(ConsumeContext<T> context)
        {
            _message = context.Message;
            _message.Action.PerformAction(_message.Body);

            return Task.FromResult(0);
        }
    }
}
