using Framework.Core;
using Framework.ServiceBus;
using Newtonsoft.Json;
using System;

namespace WebApp.API.Contracts
{
    /// <summary>
    /// Interface for draw model requests by ID
    /// </summary>
    /// <seealso cref="Framework.ServiceBus.IMessageRequest{WebApp.API.Contracts.IDrawModelContract}" />
    public interface IDrawModelRequestById : IMessageRequest<IDrawModelContract>
    {
        /// <summary>
        /// Gets the draw identifier.
        /// </summary>
        /// <value>
        /// The draw identifier.
        /// </value>
        [JsonProperty("draw_id")]
        int DrawId { get; }
    }

    /// <summary>
    /// Interface for draw model requests by latest
    /// </summary>
    /// <seealso cref="Framework.ServiceBus.IMessageRequest{WebApp.API.Contracts.IDrawModelContract}" />
    public interface IDrawModelRequestLatest : IMessageRequest<IDrawModelContract>
    {
    }
}
