using Framework.Cache;
using Framework.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WebApp.API.Contracts;

namespace WebApp.API.Models
{
    /// <summary>
    /// Powerball draw model
    /// </summary>
    [CachedEntity("RabbitLottoDraw")]
    [QueuedEntity("RabbitLottoDraw")]
    public class RabbitLottoDrawModel
    {
        /// <summary>
        /// The draw winning numbers
        /// </summary>
        [JsonProperty("draw_winning_numbers")]
        public IEnumerable<int> DrawWinningNumbers;

        /// <summary>
        /// The draw date time
        /// </summary>
        [JsonProperty("draw_date_time")]
        public DateTime DrawDateTime;

        /// <summary>
        /// The draw number
        /// </summary>
        [JsonProperty("draw_number")]
        public int DrawNumber;

        /// <summary>
        /// The draw status
        /// </summary>
        [JsonProperty("draw_status")]
        public DrawStatusCode DrawStatus;
    }
}