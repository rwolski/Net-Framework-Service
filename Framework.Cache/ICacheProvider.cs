namespace Core
{
    public interface ICacheProvider
    {
        ICacheStore GetStore(int storeIdx = 0);
    }
}