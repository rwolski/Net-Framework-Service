using Newtonsoft.Json;
using System;

namespace Framework.Core
{
    public interface IEquals
    {
        bool Equals(object obj);

        int GetHashCode();
    }

    public interface IAuditable
    {
        [JsonProperty("created_date_time")]
        DateTime? CreatedDateTime { get; }

        [JsonProperty("updated_date_time")]
        DateTime? UpdatedDateTime { get; }

        [JsonProperty("version")]
        int Version { get; }
    }
}
