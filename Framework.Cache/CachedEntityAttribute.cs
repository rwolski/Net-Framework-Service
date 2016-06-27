using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CachedEntityAttribute : Attribute
    {
        public string CacheName { get; set; }

        public CachedEntityAttribute()
        {
        }

        public CachedEntityAttribute(string cacheName)
        {
            CacheName = cacheName;
        }
    }
}
