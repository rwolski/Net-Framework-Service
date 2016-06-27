using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class PersistedEntityAttribute : Attribute
    {
        readonly string _entityName;

        public PersistedEntityAttribute(string entityName)
        {
            _entityName = entityName;
        }

        public PersistedEntityAttribute()
        {
        }
    }
}
