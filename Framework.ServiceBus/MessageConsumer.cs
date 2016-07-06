using MassTransit;
using MassTransit.Pipeline;
using System;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public class MyConsumerFactory<TConsumer> : IConsumerFactory<TConsumer> where TConsumer : class, new()
    {
        public async Task Send<T>(ConsumeContext<T> context, IPipe<ConsumerConsumeContext<TConsumer, T>> next) where T : class
        {
            TConsumer consumer = null;
            try
            {
                consumer = new TConsumer();
                var a = context.PushConsumer(consumer);
                await next.Send(a).ConfigureAwait(false);
            }
            finally
            {
                var disposable = consumer as IDisposable;
                disposable?.Dispose();
            }
        }

        public void Probe(ProbeContext context)
        {
            context.CreateConsumerFactoryScope<TConsumer>("defaultConstructor");
        }
    }

    //public class MessageConsumer : IConsumer<IServiceData>, IConsumer<ServiceData>
    //{
    //    //IServiceData _message { get; set; }

    //    public MessageConsumer()
    //    {
    //        var i = 3;
    //    }

    //    public Task Consume(ConsumeContext<IServiceData> context)
    //    {
    //        var i = 3;
    //        //_message = context.Message;
    //        //_message.Action();
    //        //_message.PerformAction();

    //        return Task.FromResult(0);
    //    }

    //    public Task Consume(ConsumeContext<ServiceData> context)
    //    {
    //        //_message = context.Message;
    //        context.Message.Action();
    //        //_message.PerformAction();

    //        return Task.FromResult(0);
    //    }
    //}

    public class MessageConsumer<T> : IConsumer<T> where T : class, IServiceData
    {
        //IServiceData _message { get; set; }

        public MessageConsumer()
        {
            var i = 3;
        }

        public Task Consume(ConsumeContext<T> context)
        {
            var i = 3;
            //_message = context.Message;
            //_message.Action();
            //_message.PerformAction();

            return Task.FromResult(0);
        }
    }
}
