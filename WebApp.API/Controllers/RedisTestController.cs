using Framework.Cache;
using Framework.Queue;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApp.API.Controllers
{
    /// <summary>
    /// Redis test controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/redistest")]
    public class RedisTestController : ApiController
    {
        readonly ICacheStore _cacheStore;
        readonly ISimpleQueueProvider _queueProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisTestController"/> class.
        /// </summary>
        public RedisTestController(ICacheStore cacheStore, ISimpleQueueProvider queueProvider)
        {
            if (cacheStore == null)
                throw new ArgumentNullException("cacheStore");
            if (queueProvider == null)
                throw new ArgumentNullException("queueProvider");

            _cacheStore = cacheStore;
            _queueProvider = queueProvider;
        }


        /// <summary>
        /// Gets the latest model (GET api/redistest).
        /// </summary>
        /// <returns></returns>
        public async Task<RedisTestModel> Get()
        {
            var model = await _cacheStore.GetObject<RedisTestModel>();
            if (model != null)
            {
                return model;
            }

            using (var q = _queueProvider.GetQueue<RedisTestModel>())
            {
                model = await q.Receive();
                if (model != null)
                    await _cacheStore.SetObject(model);

                return model;
            }
        }
    }
}