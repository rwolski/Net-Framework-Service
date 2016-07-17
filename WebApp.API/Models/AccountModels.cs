using Framework.Data;
using Framework.Queue;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace WebApp.API.Models
{
    /// <summary>
    /// Account model
    /// </summary>
    [PersistedEntity("AccountMongo")]
    [QueuedEntity("AccountQueue")]
    public class AccountModel : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountModel"/> class.
        /// </summary>
        public AccountModel()
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
                return "Account";
            }
        }

        /// <summary>
        /// Username
        /// </summary>
        [IndexField("username")]
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [EntityField("password")]
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        [EntityField("gender")]
        [JsonProperty("gender")]
        public GenderCode Gender { get; set; }

        #endregion
    }

    /// <summary>
    /// Gender codes
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GenderCode
    {
        /// <summary>
        /// Unknown gender
        /// </summary>
        [EnumMember(Value = "unknown")]
        Unknown = 0,

        /// <summary>
        /// Male
        /// </summary>
        [EnumMember(Value = "male")]
        Male = 1,

        /// <summary>
        /// Female
        /// </summary>
        [EnumMember(Value = "female")]
        Female = 2
    }
}