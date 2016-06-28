using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Data
{
    public class MongoDatabaseProvider : IDatabaseProvider
    {
        readonly MongoClient _client;
        const string connectionString = "mongodb://{0}:{1}";

        public MongoDatabaseProvider(string hostname = "localhost", int port = 27017)
        {
            if (string.IsNullOrWhiteSpace(hostname))
                throw new ArgumentNullException("hostname");
            if (port <= 0)
                throw new ArgumentOutOfRangeException("port");

            _client = new MongoClient(string.Format(connectionString, hostname, port));
        }

        public IDatabaseConnection GetDatabase(string database)
        {
            if (string.IsNullOrWhiteSpace(database))
                throw new ArgumentNullException("database");

            return new MongoDatabaseConnection(_client.GetDatabase(database));
        }

        public IDatabaseConnection GetDatabase<T>()
        {
            var databaseName = GetDatabaseFromType<T>();
            return new MongoDatabaseConnection(_client.GetDatabase(databaseName));
        }

        private string GetDatabaseFromType<T>()
        {
            var attr = typeof(T).GetCustomAttributes(typeof(PersistedEntityAttribute), false).FirstOrDefault() as PersistedEntityAttribute;
            if (attr == null || string.IsNullOrWhiteSpace(attr.EntityName))
                return typeof(T).Name;

            return attr.EntityName;
        }
    }
}
