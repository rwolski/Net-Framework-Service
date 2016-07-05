using System;

namespace Framework.Queue
{
    public class QueuedEntityAttribute : Attribute
    {
        public string EntityName { get; private set; }

        public QueuedEntityAttribute(string entityName)
        {
            EntityName = entityName;
        }

        public QueuedEntityAttribute()
        {
        }
    }
}