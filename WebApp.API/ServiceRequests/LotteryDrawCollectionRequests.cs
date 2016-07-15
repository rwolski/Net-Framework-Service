using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.API.Contracts;

namespace WebApp.API
{
    /// <summary>
    /// Request for latest collection of lottery draw models
    /// </summary>
    /// <seealso cref="WebApp.API.Contracts.IDrawModelRequestById" />
    public class LotteryDrawCollectionRequestLatest : IDrawCollectionRequestLatest
    {
        /// <summary>
        /// Gets the limit.
        /// </summary>
        /// <value>
        /// The limit.
        /// </value>
        [JsonProperty("limit")]
        public int limit { get; set; }
    }
}