namespace Framework.Cache
{
    public interface ICacheProvider
    {
        ICacheStore GetStore(int storeIdx = 0);
    }
}