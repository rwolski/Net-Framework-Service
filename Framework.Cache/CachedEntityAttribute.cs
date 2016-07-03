using System;

namespace Framework.Cache
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
