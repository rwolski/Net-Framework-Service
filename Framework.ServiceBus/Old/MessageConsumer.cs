using Autofac;
using MassTransit;
using MassTransit.Pipeline;
using System;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    //public class MyConsumerFactory<TConsumer> : IConsumerFactory<TConsumer> where TConsumer : class, new()
    //{
    //    ILifetimeScope _scope;

    //    public MyConsumerFactory(ILifetimeScope scope)
    //    {
    //        _scope = scope;
    //    }

    //    public async Task Send<T>(ConsumeContext<T> context, IPipe<ConsumerConsumeContext<TConsumer, T>> next) where T : class
    //    {
    //        TConsumer consumer = null;
    //        try
    //        {
    //            consumer = _scope.Resolve<TConsumer>();
    //            //consumer = new TConsumer();
    //            var a = context.PushConsumer(consumer);
    //            await next.Send(a).ConfigureAwait(false);
    //        }
    //        finally
    //        {
    //            var disposable = consumer as IDisposable;
    //            disposable?.Dispose();
    //        }
    //    }

    //    public void Probe(ProbeContext context)
    //    {
    //        context.CreateConsumerFactoryScope<TConsumer>("defaultConstructor");
    //    }
    //}

    public class MessageConsumer<T> : IConsumer<T>, IDisposable where T : class, IServiceEvent
    {
        ILifetimeScope _scope;

        public MessageConsumer(ILifetimeScope scope)
        {
            _scope = scope.BeginLifetimeScope();
        }

        public Task Consume(ConsumeContext<T> context)
        {
            context.Message.Scope = _scope;
            return context.Message.Action();
        }

        ~MessageConsumer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (disposing)
            {
                _scope.Dispose();
            }
        }

    }
}
