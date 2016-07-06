namespace Framework.ServiceBus
{
    public class ServiceData : IServiceData
    {
        public int Val { get; set; }

        public virtual void Action()
        {
            var i = 3;
        }
    }
}