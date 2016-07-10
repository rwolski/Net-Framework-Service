using Autofac;
using MassTransit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public class RequestMessageConsumer<T> : IConsumer<T> where T : class
    {
        readonly ILifetimeScope _scope;

        public RequestMessageConsumer(ILifetimeScope scope)
        {
            if (scope == null)
                throw new ArgumentNullException("scope");

            _scope = scope;
        }

        public async Task Consume(ConsumeContext<T> context)
        {
            var contractType = typeof(T).GetInterfaces().SelectMany(x => x.GetGenericArguments()).FirstOrDefault();
            var handler = _scope.Resolve<IMessageRequestHandler<T>>();
            var result = await handler.Request(context.Message);
            await context.RespondAsync(result, contractType);
        }
    }
}
