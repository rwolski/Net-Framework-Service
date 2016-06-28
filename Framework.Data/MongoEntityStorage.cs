using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Data
{
    public class MongoEntityStorage<T> : IEntityStorage<T> where T : IEntity
    {
        readonly IDatabaseConnection _connection;
        readonly IMongoCollection<T> _collection;


        public MongoEntityStorage(IDatabaseConnection connection)
        {
            _connection = connection;
            _collection = _connection.GetCollection<T>(typeof(T).Name);
        }

        public void Save(T entity)
        {
            if (entity.Id == null)
            {
                _collection.InsertOne(entity);
            }
            else
            {
                var update = BuildUpdatedFields(entity);
                _collection.UpdateOne(x => x.Id == entity.Id, update, new UpdateOptions()
                {
                    IsUpsert = true
                });
            }
        }

        public void Delete(T entity)
        {
            if (entity.Id == null)
                throw new InvalidOperationException("Cannot delete entities that haven't been persisted");

            Delete(entity.Id);
        }

        public void Delete(object id)
        {
            _collection.DeleteOne(x => x.Id == id);
        }

        public T FindByIdentity(object id)
        {
            return _collection.Find(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return _collection.Find(expression).ToEnumerable();
        }

        public IEnumerable<T> FindAll()
        {
            return _collection.Find(new BsonDocument() { }).ToEnumerable();
        }

        private UpdateDefinition<T> BuildUpdatedFields(T entity)
        {
            UpdateDefinitionBuilder<T> builder = Builders<T>.Update;

            var props = typeof(T).GetProperties().Select(x => new
            {
                Property = x,
                EntityMapping = x.GetCustomAttributes(typeof(EntityFieldAttribute), true).Cast<EntityFieldAttribute>().FirstOrDefault()
            }).Where(x => x.EntityMapping != null);

            foreach (var prop in props)
            {
                builder.Set(prop.EntityMapping.FieldName ?? prop.Property.Name, prop.Property.GetValue(entity));
            }

            return builder.ToBsonDocument();
        }
    }
}
