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
        public bool Incrementing { get; set; }
        public bool StringRepresentation { get; set; }

        public EntityFieldAttribute(string fieldName = null)
        {
            FieldName = fieldName;
            Incrementing = false;
            StringRepresentation = false;
        }
    }

    public class IdFieldAttribute : EntityFieldAttribute
    {
        public IdFieldAttribute(string fieldName = null)
            : base(fieldName)
        {
        }
    }

    //public class VersionFieldAttribute : EntityFieldAttribute
    //{
    //    public VersionFieldAttribute(string fieldName = null)
    //        : base(fieldName)
    //    {
    //    }
    //}

    public class FieldIgnoreAttribute : Attribute
    {
    }

    public class IndexFieldAttribute : EntityFieldAttribute
    {
        public int Sequence { get; set; }
        public bool IsAscending { get; set; }

        public IndexFieldAttribute(string fieldName = null, int sequence = 0, bool ascending = true)
            : base(fieldName)
        {
            Sequence = sequence;
            IsAscending = ascending;
        }
    }
}
