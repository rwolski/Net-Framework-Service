using MassTransit;
using System.Threading.Tasks;

namespace Framework.Queue
{
    public class MassTransitMessage<T> : IQueueMessage
    {
        public virtual void PerformAction()
        {
        }
    }
}
