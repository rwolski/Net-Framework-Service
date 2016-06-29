using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Framework.Data
{
    /// <summary>
    /// Base entity class
    /// </summary>
    /// <seealso cref="Core.IEntity" />
    [PersistedEntity]
    public abstract class Entity : IEntity, IAuditable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        public Entity()
        {
            CreatedDateTime = DateTime.Now;
            Version = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Entity(long id)
            : base()
        {
            Id = id;
        }

        /// <summary>
        /// Gets the entity identifier.
        /// </summary>
        /// <value>
        /// The entity identifier.
        /// </value>
        [IdField("_id")]
        public object Id { get; set; }

        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        /// <value>
        /// The name of the entity.
        /// </value>
        [FieldIgnore]
        public virtual string EntityName
        {
            get
            {
                return GetType().Name;
            }
        }

        /// <summary>
        /// Gets the created date time.
        /// </summary>
        /// <value>
        /// The created date time.
        /// </value>
        [EntityField("created_date_time")]
        public virtual DateTime? CreatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the updated date time.
        /// </summary>
        /// <value>
        /// The updated date time.
        /// </value>
        [EntityField("updated_date_time")]
        public virtual DateTime? UpdatedDateTime { get; set; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [EntityField("version", Incrementing = true)]
        public virtual int Version { get; set; }


        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        //public override abstract bool Equals(object obj);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        //public override abstract int GetHashCode();
    }
}
