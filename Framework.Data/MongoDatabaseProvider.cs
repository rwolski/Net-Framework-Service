using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Data
{
    public class MongoDatabaseProvider : IDatabaseProvider
    {
        readonly MongoClient _client;
        readonly string _defaultDatabase;
        const string connectionString = "mongodb://{0}:{1}";


        public MongoDatabaseProvider(string hostname = "localhost", int port = 27017, string defaultDatabase = null)
        {
            if (string.IsNullOrWhiteSpace(hostname))
                throw new ArgumentNullException("hostname");
            if (port <= 0)
                throw new ArgumentOutOfRangeException("port");

            _client = new MongoClient(string.Format(connectionString, hostname, port));
            _defaultDatabase = defaultDatabase;
        }

        public IDatabaseConnection GetDatabase(string database)
        {
            if (string.IsNullOrWhiteSpace(database))
                throw new ArgumentNullException("database");

            return new MongoDatabaseConnection(_client.GetDatabase(database));
        }

        public IDatabaseConnection GetDatabase()
        {
            if (string.IsNullOrWhiteSpace(_defaultDatabase))
                throw new InvalidOperationException("Cannot initialize default database without database name");

            return new MongoDatabaseConnection(_client.GetDatabase(_defaultDatabase));
        }

        public Task DropDatabaseAsync(string database)
        {
            return _client.DropDatabaseAsync(database);
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
