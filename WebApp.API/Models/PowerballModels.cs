using Framework.Cache;
using Framework.Queue;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WebApp.API.Models
{
    /// <summary>
    /// Powerball draw model
    /// </summary>
    [CachedEntity("PowerballDrawCache")]
    [QueuedEntity("PowerballDrawQueue")]
    public class PowerballDrawModel
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

    /// <summary>
    /// Draw status codes
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DrawStatusCode
    {
        /// <summary>
        /// Draw is closed
        /// </summary>
        [EnumMember(Value = "closed")]
        Closed = 0,

        /// <summary>
        /// Draw is open
        /// </summary>
        [EnumMember(Value = "open")]
        Open = 1
    }
}