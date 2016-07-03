using System.Threading.Tasks;

namespace Framework.Data
{
    public interface IDatabaseProvider
    {
        IDatabaseConnection GetDatabase(string database);

        IDatabaseConnection GetDatabase();

        Task DropDatabaseAsync(string database);
    }
}
