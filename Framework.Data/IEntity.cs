using MongoDB.Bson;
using Newtonsoft.Json;
using System;

namespace Framework.Data
{
    public interface IEntity
    {
        [JsonProperty("id")]
        object Id { get; }

        [JsonProperty("entity_name")]
        string EntityName { get; }
    }
}
