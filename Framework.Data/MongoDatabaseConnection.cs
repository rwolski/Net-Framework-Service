using Framework.API;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Data
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

        public IEntityStorage<T> GetCollection<T>(string entityType) where T : Entity
        {
            var collection = _database.GetCollection<T>(entityType);

            // Ensure the index exists
            var index = BuildIndex<T>();
            if (index != null)
                collection.Indexes.CreateOneAsync(index);

            return new MongoEntityStorage<T>(collection);
        }

        public IEntityStorage<T> GetCollection<T>() where T : Entity
        {
            var entityName = GetDatabaseFromType<T>();
            return GetCollection<T>(entityName);
        }

        public IStorage GetCollection(string name)
        {
            var collection = _database.GetCollection<BsonDocument>(name);

            return new MongoStorage(collection);
        }

        public Task DropCollection(string entityType)
        {
            return _database.DropCollectionAsync(entityType);
        }

        private string GetDatabaseFromType<T>()
        {
            var attr = typeof(T).GetCustomAttributes(typeof(PersistedEntityAttribute), false).FirstOrDefault() as PersistedEntityAttribute;
            if (attr == null || string.IsNullOrWhiteSpace(attr.EntityName))
                return typeof(T).Name;

            return attr.EntityName;
        }

        private IndexKeysDefinition<T> BuildIndex<T>()
        {
            var builder = Builders<T>.IndexKeys;
            var indexFields = typeof(T).GetProperties().Select(x => new
            {
                Property = x,
                Index = x.GetCustomAttributes(typeof(IndexFieldAttribute), true).Cast<IndexFieldAttribute>().FirstOrDefault(),
                Entity = x.GetCustomAttributes(typeof(EntityFieldAttribute), true).Cast<EntityFieldAttribute>().FirstOrDefault()
            }).Where(x => x.Index != null).OrderBy(x => x.Index.Sequence);

            if (!indexFields.Any())
                return null;

            foreach (var field in indexFields)
            {
                if (field.Index.IsAscending)
                {
                    builder.Ascending(field.Entity.FieldName ?? field.Property.Name);
                }
                else
                {
                    builder.Descending(field.Entity.FieldName ?? field.Property.Name);
                }
            }

            return builder.ToBsonDocument();
        }
    }
}
