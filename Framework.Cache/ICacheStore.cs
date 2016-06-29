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

        string GetString(string key);

        IEnumerable<string> GetList(string key);

        T GetObject<T>(string key);

        T GetObject<T>();

        #endregion

        #region Set

        void SetString(string key, string val, int expiryMinutes = 60);

        void SetList(string key, IEnumerable<string> list, int expiryMinutes = 60);

        void SetObject<T>(string key, T obj, int expiryMinutes = 60);

        void SetObject<T>(T obj, int expiryMinutes = 60);

        void Unset(string key);

        #endregion
    }
}
