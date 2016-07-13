using Framework.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.API.Contracts
{
    public interface IDrawCollectionContract : IMessageContract
    {
        /// <summary>
        /// Gets the draws.
        /// </summary>
        /// <value>
        /// The draws.
        /// </value>
        IDrawModelContract[] Draws { get; }
    }
}
