using Framework;
using Framework.Data;
using WebApp.API.Models;

namespace WebApp.API
{
    public class MongoConfig
    {
        public static void RegisterTypes()
        {
            MongoEntityMapper.Map<Entity>();
            MongoEntityMapper.Map<OzLottoDrawModel>();

        }
    }
}