using MongoDB.Driver;

namespace Framework.Data
{
    public interface IDatabaseConnection
    {
        IMongoCollection<T> GetCollection<T>(string entityType);
    }
}
