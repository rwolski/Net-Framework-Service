using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class EntityFieldAttribute : Attribute
    {
        public string FieldName { get; private set; }
        public bool IdField { get; set; }

        public EntityFieldAttribute(string fieldName)
        {
            FieldName = fieldName;
        }

        public EntityFieldAttribute()
        {
        }
    }

    public class IdFieldAttribute : EntityFieldAttribute
    {
        public IdFieldAttribute(string fieldName)
            : base(fieldName)
        {
        }

        public IdFieldAttribute()
        {
        }
    }

    public class IgnoreField : Attribute
    {

    }
}
