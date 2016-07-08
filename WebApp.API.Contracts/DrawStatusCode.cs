using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace WebApp.API
{
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