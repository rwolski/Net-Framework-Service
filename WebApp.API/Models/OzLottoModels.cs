using Framework.Data;
using Framework.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WebApp.API.Models
{
    /// <summary>
    /// Oz lotto draw model
    /// </summary>
    [PersistedEntity("OzLottoDrawMongo")]
    [QueuedEntity("OzLottoDrawQueue")]
    public class OzLottoDrawModel : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OzLottoDrawModel"/> class.
        /// </summary>
        public OzLottoDrawModel()
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
                return "OzLotto";
            }
        }

        /// <summary>
        /// The draw winning numbers
        /// </summary>
        [EntityField("draw_winning_numbers")]
        [JsonProperty("draw_winning_numbers")]
        public IEnumerable<int> DrawWinningNumbers { get; set; }

        /// <summary>
        /// The draw date time
        /// </summary>
        [IndexField("draw_date_time", Sequence = 1)]
        [JsonProperty("draw_date_time")]
        public DateTime DrawDateTime { get; set; }

        /// <summary>
        /// The draw number
        /// </summary>
        [IndexField("draw_number", Sequence = 2)]
        [JsonProperty("draw_number")]
        public int DrawNumber { get; set; }

        /// <summary>
        /// The draw status
        /// </summary>
        [EntityField("draw_status")]
        [JsonProperty("draw_status")]
        public DrawStatusCode DrawStatus { get; set; }

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

            return Equals((OzLottoDrawModel)obj);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(OzLottoDrawModel other)
        {
            return this.DrawNumber == other.DrawNumber;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return this.DrawNumber.GetHashCode();
        }

        #endregion
    }
}