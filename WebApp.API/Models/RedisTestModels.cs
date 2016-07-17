using Framework.Cache;
using Framework.Queue;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WebApp.API
{
    /// <summary>
    /// Redis test model
    /// </summary>
    [CachedEntity("RedisTestModel")]
    [QueuedEntity("RedisTestModel")]
    public class RedisTestModel
    {
        /// <summary>
        /// The numbers
        /// </summary>
        [JsonProperty("numbers")]
        public IEnumerable<int> Numbers;

        /// <summary>
        /// The date time
        /// </summary>
        [JsonProperty("date_time")]
        public DateTime DateTime;

        /// <summary>
        /// The number
        /// </summary>
        [JsonProperty("number")]
        public int Number;

        /// <summary>
        /// The status
        /// </summary>
        [JsonProperty("status")]
        public RedisStatusCode Status;
    }

    /// <summary>
    /// Draw status codes
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RedisStatusCode
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