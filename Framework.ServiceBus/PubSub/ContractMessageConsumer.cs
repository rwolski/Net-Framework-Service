using Autofac;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public class ContractMessageConsumer<T> : IConsumer<T> where T : class
    {
        readonly ILifetimeScope _scope;

        public ContractMessageConsumer(ILifetimeScope scope)
        {
            if (scope == null)
                throw new ArgumentNullException("scope");

            _scope = scope;
        }

        public Task Consume(ConsumeContext<T> context)
        {
            var handler = _scope.Resolve<IMessageAction<T>>(new TypedParameter(typeof(T), context.Message));
            return handler.Action();
        }
    }
}
