namespace Core
{
    public interface IEntity
    {
        long? EntityId { get; }

        string EntityName { get; }
    }
}
