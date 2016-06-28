using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Cache
{
    public sealed class RedisProvider : ICacheProvider
    {
        readonly ConnectionMultiplexer _redis;
        const string connectionString = "{0}:{1}";

        public RedisProvider(string hostname = "localhost", int port = 6379)
        {
            if (string.IsNullOrWhiteSpace(hostname))
                throw new ArgumentNullException("hostname");
            if (port <= 0)
                throw new ArgumentOutOfRangeException("port");

            _redis = ConnectionMultiplexer.Connect(string.Format(connectionString, hostname, port));
        }

        public ICacheStore GetStore(int storeIdx = 0)
        {
            return new RedisCacheStore(_redis, storeIdx);
        }
    }
}
