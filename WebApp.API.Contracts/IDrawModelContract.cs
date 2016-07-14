using Framework.Core;
using Framework.Data;
using Framework.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WebApp.API.Contracts
{
    [JsonObject("IDrawModelContract")]
    public interface IDrawModelContract : IMessageContract, IEntity, IAuditable
    {
        [JsonProperty("draw_winning_numbers")]
        IEnumerable<int> DrawWinningNumbers { get; }

        /// <summary>
        /// The draw date time
        /// </summary>
        [JsonProperty("draw_date_time")]
        DateTime DrawDateTime { get; }

        /// <summary>
        /// The draw number
        /// </summary>
        [JsonProperty("draw_number")]
        int DrawNumber { get; }

        /// <summary>
        /// The draw status
        /// </summary>
        [JsonProperty("draw_status")]
        DrawStatusCode DrawStatus { get; }
    }
}
