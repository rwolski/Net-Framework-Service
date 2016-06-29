using MongoDB.Bson;
using System;

namespace Framework.Data
{
    public interface IEntity
    {
        object Id { get; }

        string EntityName { get; }
    }

    public interface IEquals
    {
        bool Equals(object obj);

        int GetHashCode();
    }

    public interface IAuditable
    {
        DateTime? CreatedDateTime { get; }

        DateTime? UpdatedDateTime { get; set; }

        int Version { get; }
    }
}
