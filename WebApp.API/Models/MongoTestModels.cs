using Framework.Data;
using Framework.Queue;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WebApp.API.Models
{
    /// <summary>
    /// Mongo test model
    /// </summary>
    [PersistedEntity("MongoTestModel")]
    [QueuedEntity("MongoTestModel")]
    public class MongoTestModel : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoTestModel"/> class.
        /// </summary>
        public MongoTestModel()
        {
        }

        #region Members

        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        [FieldIgnore]
        [JsonIgnore]
        public override string EntityName
        {
            get
            {
                return "MongoTestModel";
            }
        }

        /// <summary>
        /// The magic numbers
        /// </summary>
        [EntityField("numbers")]
        [JsonProperty("numbers")]
        public IEnumerable<int> Numbers { get; set; }

        /// <summary>
        /// The date time
        /// </summary>
        [IndexField("date_time", Sequence = 1)]
        [JsonProperty("date_time")]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// The number
        /// </summary>
        [IndexField("number", Sequence = 2)]
        [JsonProperty("number")]
        public int Number { get; set; }

        /// <summary>
        /// The status
        /// </summary>
        [EntityField("status")]
        [JsonProperty("status")]
        public MongoStatusCode Status { get; set; }

        #endregion

        #region IEquals

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return Equals((MongoTestModel)obj);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(MongoTestModel other)
        {
            return this.Number == other.Number;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return this.Number.GetHashCode();
        }

        #endregion
    }

    /// <summary>
    /// Draw status codes
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MongoStatusCode
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