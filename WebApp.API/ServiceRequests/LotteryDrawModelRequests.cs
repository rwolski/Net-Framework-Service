using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.API.Contracts;

namespace WebApp.API
{
    /// <summary>
    /// Request for lottery draw model by ID
    /// </summary>
    /// <seealso cref="WebApp.API.Contracts.IDrawModelRequestById" />
    public class LotteryDrawModelRequestById : IDrawModelRequestById
    {
        /// <summary>
        /// Gets the draw identifier.
        /// </summary>
        /// <value>
        /// The draw identifier.
        /// </value>
        [JsonProperty("draw_id")]
        public Guid DrawId { get; set; }
    }

    /// <summary>
    /// Request for the latest lottery draw model
    /// </summary>
    /// <seealso cref="WebApp.API.Contracts.IDrawModelRequestLatest" />
    public class LotteryDrawModelRequestLast : IDrawModelRequestLatest
    {
    }
}