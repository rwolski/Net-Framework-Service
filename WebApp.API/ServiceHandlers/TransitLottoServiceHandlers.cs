using Autofac;
using Framework.Cache;
using Framework.WebSockets;
using System.Threading.Tasks;
using WebApp.API.Models;
using WebApp.API.Contracts;

namespace WebApp.API.ServiceHandler
{
    /// <summary>
    /// Transit draw model based on event
    /// </summary>
    /// <seealso cref="Framework.Data.Entity" />
    /// <seealso cref="WebApp.API.Contracts.ITransitLottoDrawEvent" />
    public class TransitLottoDrawEventServiceHandler : TransitLottoDrawModel, ITransitLottoDrawEvent
    {
        /// <summary>
        /// Gets or sets the scope.
        /// </summary>
        /// <value>
        /// The scope.
        /// </value>
        public ILifetimeScope Scope { get; set; }

        /// <summary>
        /// Actions this instance.
        /// </summary>
        /// <returns></returns>
        public Task Action()
        {
            var cacheStore = Scope.Resolve<ICacheStore>();
            cacheStore.SetObject<ITransitLottoDrawEvent>(this);

            var webSocket = Scope.Resolve<ISocketProvider>();
            //var hub = webSocket.GetHub();
            //hub.Broadcast("host", JsonConvert.SerializeObject(this));

            return Task.FromResult(0);
        }

    }
}