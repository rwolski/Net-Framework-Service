using Framework.Core;
using Framework.ServiceBus;
using Newtonsoft.Json;
using System;

namespace WebApp.API.Contracts
{
    /// <summary>
    /// Interface for getting a collection of recent draw models
    /// </summary>
    /// <seealso cref="Framework.ServiceBus.IMessageRequest{WebApp.API.Contracts.IDrawModelContract}" />
    public interface IDrawCollectionRequestLatest : IMessageRequest<IDrawCollectionContract>
    {
        /// <summary>
        /// Gets the limit.
        /// </summary>
        /// <value>
        /// The limit.
        /// </value>
        [JsonProperty("limit")]
        int limit { get; }
    }
}
