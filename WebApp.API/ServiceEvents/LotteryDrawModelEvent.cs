using Framework.Cache;
using Framework.ServiceBus;
using Framework.WebSockets;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WebApp.API.Contracts;

namespace WebApp.API.ServiceEvents
{
    /// <summary>
    /// Event handler for lotter draw event
    /// </summary>
    public class LotteryDrawModelEvent : MessageEventHandler<IDrawModelContract>
    {
        readonly ICacheStore _cache;
        readonly ISocketProvider _webSockets;

        /// <summary>
        /// Initializes a new instance of the <see cref="LotteryDrawModelEvent" /> class.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <param name="cache">The cache.</param>
        /// <param name="webSockets">The web sockets.</param>
        /// <exception cref="System.ArgumentNullException">
        /// cache
        /// or
        /// webSockets
        /// </exception>
        public LotteryDrawModelEvent(IDrawModelContract contract, ICacheStore cache, ISocketProvider webSockets)
            : base(contract)
        {
            if (cache == null)
                throw new ArgumentNullException("cache");
            if (webSockets == null)
                throw new ArgumentNullException("webSockets");
            _cache = cache;
            _webSockets = webSockets;
        }

        /// <summary>
        /// Performs the event action.
        /// </summary>
        /// <returns></returns>
        public override async Task Handle()
        {
            // Update the cache with this event data
            var oldDraw = _cache.GetObject<IDrawModelContract>();
            _cache.SetObject(Contract);

            if (Contract.DrawStatus == DrawStatusCode.Closed && (await oldDraw).DrawStatus != Contract.DrawStatus)
            {
                // Broadcast the recently closed draw result
                var hub = _webSockets.GetHub();
                hub.Broadcast("host", JsonConvert.SerializeObject(this.Contract));
            }
        }
    }
}