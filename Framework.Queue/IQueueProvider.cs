using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Queue
{
    public interface IQueueProvider
    {
        IQueue GetQueue(string queueName);

        IQueue GetQueue<T>();
    }
}
