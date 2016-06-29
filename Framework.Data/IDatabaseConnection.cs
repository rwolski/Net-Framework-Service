using System.Threading.Tasks;

namespace Framework.Data
{
    public interface IDatabaseConnection
    {
        IEntityStorage<T> GetCollection<T>(string entityType) where T : Entity;

        IEntityStorage<T> GetCollection<T>() where T : Entity;

        IStorage GetCollection(string name);

        Task DropCollection(string entityType);
    }
}
