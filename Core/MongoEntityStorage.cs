using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class MongoEntityStorage<T> : IEntityStorage<T> where T : IEntity
    {
        readonly MongoDatabaseConnection _connection;
        readonly IMongoCollection<T> _collection;


        MongoEntityStorage(MongoDatabaseConnection connection)
        {
            _connection = connection;
            _collection = _connection.GetCollection<T>(typeof(T).Name);
        }

        public void Save(T entity)
        {
            if (!entity.EntityId.HasValue)
            {
                _collection.InsertOne(entity);
            }
            else
            {
                var filter = Builders<T>.Filter.Eq("EntityId", entity.EntityId.Value);
                var update = Builders<T>.Update.Set("_collection.);
                _collection.UpdateOne(filter, );
                _collection.Update
            }
        }

        public void Delete(T entity)
        {
            if (!entity.EntityId.HasValue)
                throw new InvalidOperationException("Cannot delete entities that haven't been persisted");

            Delete(entity.EntityId.Value);
        }

        public void Delete(long id)
        {
            var filter = Builders<T>.Filter.Eq("EntityId", id);
            _collection.DeleteOne(filter);
        }

        public T GetByIdentity(long id)
        {
            return _collection.Find(x => x.EntityId == id).FirstOrDefault();
        }
    }
}
