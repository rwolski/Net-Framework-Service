using Framework.Core;
using Framework.ServiceBus;
using System;

namespace WebApp.API.Contracts
{
    public interface IDrawModelRequestByIdHandler : IMessageRequestHandler<IDrawModelRequestById>
    {
    }

    public interface IDrawModelRequestLatestHandler : IMessageRequestHandler<IDrawModelRequestLatest>
    {
    }
}
