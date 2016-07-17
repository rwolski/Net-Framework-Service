using Framework.Data;
using System.Linq;

namespace WebApp.API
{
    /// <summary>
    /// Mongo module registration
    /// </summary>
    public class MongoConfig
    {
        /// <summary>
        /// Registers the types.
        /// </summary>
        public static void RegisterTypes()
        {
            var entityTypes = typeof(MongoConfig).Assembly.GetTypes().Where(x => x.IsDefined(typeof(PersistedEntityAttribute), false));

            foreach (var type in entityTypes)
                MongoEntityMapper.Map(type);
        }
    }
}