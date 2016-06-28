using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IMongoCollection<T> GetCollection<T>(string entityType)
        {
            var collection = _database.GetCollection<T>(entityType);

            // Ensure the index exists
            var index = BuildIndex<T>();
            if (index != null)
                collection.Indexes.CreateOneAsync(index);

            return collection;
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
