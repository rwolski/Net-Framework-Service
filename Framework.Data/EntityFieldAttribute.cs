using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Data
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

    public class FieldIgnoreAttribute : Attribute
    {
    }

    public class IndexFieldAttribute : Attribute
    {
        public int Sequence { get; set; }
        public bool IsAscending { get; set; }

        public IndexFieldAttribute(int sequence, bool ascending)
        {
            Sequence = sequence;
            IsAscending = ascending;
        }

        public IndexFieldAttribute()
            : this(0, true)
        {
        }
    }
}
