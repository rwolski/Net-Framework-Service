using MassTransit;
using System.Threading.Tasks;

namespace Framework.Queue
{
    public class MessageConsumer<T> : IConsumer<T> where T : class, IQueueMessage
    {
        T _message { get; set; }

        public Task Consume(ConsumeContext<T> context)
        {
            _message = context.Message;
            _message.Action.PerformAction();

            return Task.FromResult(0);
        }
    }
}
