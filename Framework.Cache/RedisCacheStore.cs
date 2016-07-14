using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Cache
{
    internal sealed class RedisCacheStore : ICacheStore
    {
        readonly ICacheClient _client;

        public RedisCacheStore(ConnectionMultiplexer connection, int database = 0)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            var serializer = new NewtonsoftSerializer();
            var cacheClient = new StackExchangeRedisCacheClient(connection, serializer, database);

            _client = cacheClient;
        }

        #region Get

        public Task<string> GetString(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("key");

            return _client.GetAsync<string>(key);
        }

        public Task<IEnumerable<string>> GetList(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("key");

            return _client.SetMemberAsync(key).ContinueWith(ct => ct.Result.ToList() as IEnumerable<string>);
        }

        public Task<T> GetObject<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("key");

            return _client.GetAsync<T>(key);
        }

        public Task<T> GetObject<T>()
        {
            var cacheAttribute = typeof(T).GetCustomAttributes(typeof(CachedEntityAttribute), false).Cast<CachedEntityAttribute>().FirstOrDefault();
            if (cacheAttribute == null || string.IsNullOrWhiteSpace(cacheAttribute.CacheName))
                return GetObject<T>(typeof(T).Name);

            return GetObject<T>(cacheAttribute.CacheName);
        }

        #endregion

        #region Set

        public Task SetString(string key, string val, int expiryMinutes = 60)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("key");
            if (string.IsNullOrWhiteSpace(val))
                throw new ArgumentNullException("val");

            return _client.AddAsync(key, val, TimeSpan.FromMinutes(expiryMinutes));
        }

        public async Task SetList(string key, IEnumerable<string> list, int expiryMinutes = 60)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("key");
            if (list == null || list.Count() == 0)
                throw new ArgumentNullException("list");

            await _client.RemoveAsync(key);

            List<Task> results = list.Select(x => _client.SetAddAsync<string>(key, x) as Task).ToList();
            results.Add(_client.Database.KeyExpireAsync(key, TimeSpan.FromMinutes(expiryMinutes)));

            await Task.WhenAll(results);
        }

        public async Task SetObject<T>(string key, T obj, int expiryMinutes = 60)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("key");
            if (obj == null)
                throw new ArgumentNullException("obj");

            var type = obj.GetType();
            if (type.IsPrimitive || Type.GetTypeCode(type) == TypeCode.String)
                throw new InvalidOperationException("Primitive types are not supported");

            await _client.RemoveAsync(key);
            await _client.AddAsync(key, obj, TimeSpan.FromMinutes(expiryMinutes));
        }

        public async Task SetObject<T>(T obj, int expiryMinutes = 60)
        {
            var cacheAttribute = typeof(T).GetCustomAttributes(typeof(CachedEntityAttribute), false).Cast<CachedEntityAttribute>().FirstOrDefault();
            await SetObject<T>(cacheAttribute.CacheName ?? typeof(T).Name, obj, expiryMinutes);
        }

        public Task Unset(string key)
        {
            return _client.RemoveAsync(key);
        }

        #endregion
    }
}
