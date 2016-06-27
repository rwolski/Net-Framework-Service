using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class MongoDatabaseConnection : IDatabaseConnection
    {
        readonly IMongoDatabase _database;

        public MongoDatabaseConnection(IMongoDatabase database)
        {
            if (database == null)
                throw new ArgumentNullException("database");

            _database = database;
        }

        public IMongoCollection<T> GetCollection<T>(string entityType)
        {
            return _database.GetCollection<T>(entityType);
        }


        //public List<T> FindAll<T>(string entityType)
        //{
            

        //    using (var cursor = list.FindSync(new BsonDocument()))
        //    {
        //        while (cursor.MoveNext())
        //        {
        //            var batch = cursor.Current;
        //            return batch.ToList();
        //        }
        //    }

        //    return null;
        //}
    }
}
