using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Data
{
    public class PersistedEntityAttribute : Attribute
    {
        public string EntityName { get; private set; }

        public PersistedEntityAttribute(string entityName)
        {
            EntityName = entityName;
        }

        public PersistedEntityAttribute()
        {
        }
    }
}
