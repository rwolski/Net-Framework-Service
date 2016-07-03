using MassTransit;
using System.Threading.Tasks;

namespace Framework.Queue
{
    public class MassTransitMessageConsumer<T> : IConsumer
    {
        public T message { get; set; }

        public Task Consume(ConsumeContext<MassTransitMessageConsumer<T>> context)
        {
            return Task.FromResult(0);
        }
    }
}
