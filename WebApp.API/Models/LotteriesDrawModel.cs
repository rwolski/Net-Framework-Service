using Framework.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WebApp.API.Contracts;

namespace WebApp.API
{
    /// <summary>
    /// Transit draw model based on event
    /// </summary>
    /// <seealso cref="Framework.Data.Entity" />
    /// <seealso cref="WebApp.API.Contracts.ITransitLottoDrawEvent" />
    [PersistedEntity("LotteriesDraw")]
    [JsonObject("LotteriesDrawModel")]
    public class LotteriesDrawModel : Entity, IDrawModelContract
    {
        /// <summary>
        /// The draw winning numbers
        /// </summary>
        [JsonProperty("draw_winning_numbers")]
        public IEnumerable<int> DrawWinningNumbers { get; set; }

        /// <summary>
        /// The draw date time
        /// </summary>
        [IndexField(Sequence = 1)]
        [JsonProperty("draw_date_time")]
        public DateTime DrawDateTime { get; set; }

        /// <summary>
        /// The draw number
        /// </summary>
        [JsonProperty("draw_number")]
        public int DrawNumber { get; set; }

        /// <summary>
        /// The draw status
        /// </summary>
        [JsonProperty("draw_status")]
        public DrawStatusCode DrawStatus { get; set; }
    }
}