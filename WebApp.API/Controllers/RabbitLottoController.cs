using Framework.Cache;
using Framework.Queue;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using WebApp.API.Models;

namespace WebApp.API.Controllers
{
    /// <summary>
    /// Powerball draw controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    //[AutofacControllerConfiguration]
    [RoutePrefix("api/rabbitlotto")]
    public class RabbitLottoController : ApiController
    {
        readonly ICacheStore _cacheStore;
        readonly ISimpleQueueProvider _queueProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitLottoController"/> class.
        /// </summary>
        public RabbitLottoController(ICacheStore cacheStore, ISimpleQueueProvider queueProvider)
        {
            if (cacheStore == null)
                throw new ArgumentNullException("cacheStore");
            if (queueProvider == null)
                throw new ArgumentNullException("queueProvider");

            _cacheStore = cacheStore;
            _queueProvider = queueProvider;
        }


        /// <summary>
        /// Gets the latest powerball draw (GET api/powerball).
        /// </summary>
        /// <returns></returns>
        public async Task<RabbitLottoDrawModel> Get()
        {
            var drawModel = await _cacheStore.GetObject<RabbitLottoDrawModel>();
            if (drawModel != null)
            {
                return drawModel;
            }

            using (var q = _queueProvider.GetQueue<RabbitLottoDrawModel>())
            {
                drawModel = await q.Receive();
                if (drawModel != null)
                    await _cacheStore.SetObject(drawModel);

                return drawModel;
            }
        }
    }
}