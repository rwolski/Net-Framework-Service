using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
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

            var db = _client.GetDatabase(database);
            return new MongoDatabaseConnection(db);
        }
    }
}
