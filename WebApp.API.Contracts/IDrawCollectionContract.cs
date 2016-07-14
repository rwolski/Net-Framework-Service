using Framework.ServiceBus;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebApp.API.Contracts
{
    public interface IDrawCollectionContract : IMessageContract
    {
        /// <summary>
        /// Gets the draws.
        /// </summary>
        /// <value>
        /// The draws.
        /// </value>
        [JsonProperty("draws")]
        IEnumerable<IDrawModelContract> Draws { get; }
    }
}
