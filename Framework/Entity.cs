using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    /// <summary>
    /// Base entity class
    /// </summary>
    /// <seealso cref="Core.IEntity" />
    [PersistedEntity]
    public class Entity : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        public Entity()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Entity(long id)
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
        public virtual string EntityName
        {
            get
            {
                return GetType().Name;
            }
        }
    }
}
