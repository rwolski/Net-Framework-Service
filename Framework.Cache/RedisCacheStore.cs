using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public string GetString(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("key");

            return _client.Get<string>(key);
        }

        public IEnumerable<string> GetList(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("key");

            return _client.SetMember(key).Select(x => x.ToString()).ToList();
        }

        public T GetObject<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("key");

            return _client.Get<T>(key);
        }

        public T GetObject<T>()
        {
            var cacheAttribute = typeof(T).GetCustomAttributes(typeof(CachedEntityAttribute), false).Cast<CachedEntityAttribute>().FirstOrDefault();
            if (cacheAttribute == null || string.IsNullOrWhiteSpace(cacheAttribute.CacheName))
                return GetObject<T>(typeof(T).Name);

            return GetObject<T>(cacheAttribute.CacheName);
        }

        #endregion

        #region Set

        public void SetString(string key, string val, int expiryMinutes = 60)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("key");
            if (string.IsNullOrWhiteSpace(val))
                throw new ArgumentNullException("val");

            _client.Remove(key);
            _client.Add(key, val, TimeSpan.FromMinutes(expiryMinutes));
        }

        public void SetList(string key, IEnumerable<string> list, int expiryMinutes = 60)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("key");
            if (list == null || list.Count() == 0)
                throw new ArgumentNullException("list");

            _client.Remove(key);

            foreach (var val in list)
                _client.SetAdd<string>(key, val);

            _client.Database.KeyExpire(key, DateTime.Now.AddMinutes(expiryMinutes));
        }

        public void SetObject<T>(string key, T obj, int expiryMinutes = 60)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("key");
            if (obj == null)
                throw new ArgumentNullException("obj");

            var type = obj.GetType();
            if (type.IsPrimitive || Type.GetTypeCode(type) == TypeCode.String)
                throw new InvalidOperationException("Primitive types are not supported");

            _client.Remove(key);
            _client.Add(key, obj, TimeSpan.FromMinutes(expiryMinutes));
        }

        public void SetObject<T>(T obj, int expiryMinutes = 60)
        {
            var cacheAttribute = typeof(T).GetCustomAttributes(typeof(CachedEntityAttribute), false).Cast<CachedEntityAttribute>().FirstOrDefault();
            SetObject<T>(cacheAttribute.CacheName, obj, expiryMinutes);
        }

        public void Unset(string key)
        {
            _client.Remove(key);
        }

        #endregion
    }
}
