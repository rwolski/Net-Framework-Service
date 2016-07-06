namespace Framework.ServiceBus
{
    public class ServiceAction<T> : IServiceAction<T>
    {
        public T Data { get; set; }

        public void PerformAction()
        {
            var i = 3;
        }
    }
}