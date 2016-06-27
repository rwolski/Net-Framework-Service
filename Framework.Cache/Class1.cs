using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CacheWrapper<T>
    {
        T _data;
        ICacheProvider _cacheProvider;

        public CacheWrapper(T data, ICacheProvider cacheProvider)
        {
            _data = data;
            _cacheProvider = cacheProvider;
        }


    }
}
