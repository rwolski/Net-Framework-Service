using Framework.Data;
using System.Linq;

namespace WebApp.API
{
    public class MongoConfig
    {
        public static void RegisterTypes()
        {
            var entityTypes = typeof(MongoConfig).Assembly.GetTypes().Where(x => x.IsDefined(typeof(PersistedEntityAttribute), false));

            foreach (var type in entityTypes)
                MongoEntityMapper.Map(type);

            //MongoEntityMapper.Map<Entity>();
            //MongoEntityMapper.Map<OzLottoDrawModel>();

        }
    }
}