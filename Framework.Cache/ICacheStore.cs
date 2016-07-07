using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Cache
{
    public interface ICacheStore
    {
        #region Get

        Task<string> GetString(string key);

        Task<IEnumerable<string>> GetList(string key);

        Task<T> GetObject<T>(string key);

        Task<T> GetObject<T>();

        #endregion

        #region Set

        Task SetString(string key, string val, int expiryMinutes = 60);

        Task SetList(string key, IEnumerable<string> list, int expiryMinutes = 60);

        Task SetObject<T>(string key, T obj, int expiryMinutes = 60);

        Task SetObject<T>(T obj, int expiryMinutes = 60);

        Task Unset(string key);

        #endregion
    }
}
