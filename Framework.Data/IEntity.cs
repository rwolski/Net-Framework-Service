using MongoDB.Bson;
using System;

namespace Framework.Data
{
    public interface IEntity
    {
        object Id { get; }

        string EntityName { get; }
    }
}
