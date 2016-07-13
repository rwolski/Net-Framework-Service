using Framework.Cache;
using Framework.Data;
using Framework.Queue;
using Framework.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
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
    [RoutePrefix("api/lottery")]
    public class LotteriesController : ApiController
    {
        readonly ICacheStore _cacheStore;
        readonly IServiceBus _serviceBus;
        readonly IEntityStorage<LotteriesDrawModel> _drawStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="LotteriesController"/> class.
        /// </summary>
        public LotteriesController(ICacheStore cacheStore, IServiceBus serviceBus, IDatabaseConnection dataConnection)
        {
            if (cacheStore == null)
                throw new ArgumentNullException("cacheStore");
            if (serviceBus == null)
                throw new ArgumentNullException("serviceBus");
            if (dataConnection == null)
                throw new ArgumentNullException("dataConnection");

            _drawStorage = dataConnection.GetCollection<LotteriesDrawModel>();
            _cacheStore = cacheStore;
            _serviceBus = serviceBus;
        }

        /// <summary>
        /// Gets the latest lottery draw (GET api/lottery/draw).
        /// </summary>
        /// <returns></returns>
        [Route("draw")]
        public async Task<LotteriesDrawModel> Get()
        {
            // Check the cache first for the latest result
            var drawModel = await _cacheStore.GetObject<LotteriesDrawModel>();
            if (drawModel != null)
            {
                return drawModel;
            }

            // Not found, now send a request to the host layer
            var request = new LotteryDrawModelRequestLast();
            drawModel = await _serviceBus.Request<LotteryDrawModelRequestLast, LotteriesDrawModel>(request);

            return drawModel;
        }

        /// <summary>
        /// Gets the specified lottery draw (GET api/lottery/draw/{id}).
        /// </summary>
        /// <returns></returns>
        [Route("draw")]
        public async Task<LotteriesDrawModel> Get(int drawId)
        {
            // Try to get the draw data from the local database
            var drawModel = _drawStorage.FindByIdentity(drawId);
            if (drawModel != null)
            {
                return drawModel;
            }

            // Not found, now send a request to the host layer
            var request = new LotteryDrawModelRequestById()
            {
                DrawId = drawId
            };
            drawModel = await _serviceBus.Request<LotteryDrawModelRequestById, LotteriesDrawModel>(request);

            return drawModel;
        }

        /// <summary>
        /// Gets the lottery draw collection (GET api/lottery/draws).
        /// </summary>
        /// <returns></returns>
        [Route("draws")]
        public LotteriesDrawCollection GetCollection()
        {
            // Try to get the draw data from the local database
            var orderBy = new List<OrderBy<LotteriesDrawModel>>()
            {
                new OrderBy<LotteriesDrawModel>()
                {
                    Exp = x => x.Id,
                    Ascending = false
                }
            };

            var lastDraws = _drawStorage.Find(null, orderBy, 5);
            if (lastDraws != null)
            {
                return new LotteriesDrawCollection()
                {
                    Draws = lastDraws.ToArray()
                };
            }

            // Not found, now send a request to the host layer
            //var request = new LotteryDrawCollectionRequest()
            //{
            //    DrawId = drawId
            //};
            //drawModel = await _serviceBus.Request<LotteryDrawModelRequestById, LotteriesDrawModel>(request);

            //return drawModel;

            return null;
        }
    }
}