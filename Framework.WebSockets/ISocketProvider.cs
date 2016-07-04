namespace Framework.WebSockets
{
    public interface ISocketProvider
    {
        IHubHost GetHub();

        IHubHost GetHub(string hubName);
    }
}