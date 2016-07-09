using Framework.Cache;
using Framework.Queue;
using Framework.ServiceBus;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using WebApp.API.Contracts;
using WebApp.API.Models;

namespace WebApp.API.Controllers
{
    /// <summary>
    /// Powerball draw controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    //[AutofacControllerConfiguration]
    [RoutePrefix("api/transitlotto")]
    public class TransitLottoController : ApiController
    {
        readonly ICacheStore _cacheStore;
        readonly IServiceBusProvider _busProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransitLottoController"/> class.
        /// </summary>
        public TransitLottoController(ICacheStore cacheStore, IServiceBusProvider busProvider)
        {
            if (cacheStore == null)
                throw new ArgumentNullException("cacheStore");
            if (busProvider == null)
                throw new ArgumentNullException("busProvider");

            _cacheStore = cacheStore;
            _busProvider = busProvider;
        }


        /// <summary>
        /// Gets the latest powerball draw (GET api/powerball).
        /// </summary>
        /// <returns></returns>
        public async Task<ITransitLottoDrawEvent> Get()
        {
            var drawModel = await _cacheStore.GetObject<ITransitLottoDrawEvent>();
            if (drawModel != null)
            {
                return drawModel;
            }

            var bus = _busProvider.GetBus("TransitLotto");

            //drawModel = await bus.Request<RabbitLottoDrawModel>();
            return drawModel;
        }
    }
}