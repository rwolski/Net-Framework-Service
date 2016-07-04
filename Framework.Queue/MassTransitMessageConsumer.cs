using MassTransit;
using System.Threading.Tasks;

namespace Framework.Queue
{
    public class MassTransitMessageConsumer : IConsumer<IQueueMessage>
    {
        IQueueMessage _message { get; set; }

        public Task Consume(ConsumeContext<IQueueMessage> context)
        {
            _message = context.Message;
            _message.PerformAction();

            return Task.FromResult(0);
        }
    }
}
