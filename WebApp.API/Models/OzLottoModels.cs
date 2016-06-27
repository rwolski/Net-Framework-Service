using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.API.Models
{
    /// <summary>
    /// Oz lotto draw model
    /// </summary>
    [PersistedEntity("OzLotto")]
    public class OzLottoDrawModel : Entity
    {
        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        [IgnoreField]
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
        public IEnumerable<int> DrawWinningNumbers { get; set; }

        /// <summary>
        /// The draw date time
        /// </summary>
        [EntityField("draw_date_time")]
        public DateTime DrawDateTime { get; set; }

        /// <summary>
        /// The draw number
        /// </summary>
        [EntityField("draw_number")]
        public int DrawNumber { get; set; }

        /// <summary>
        /// The draw status
        /// </summary>
        [EntityField("draw_status")]
        public DrawStatusCode DrawStatus { get; set; }
    }
}