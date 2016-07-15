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
        public async Task<IDrawModelContract> Get()
        {
            // Check the cache first for the latest result
            IDrawModelContract drawModel = await _cacheStore.GetObject<IDrawModelContract>();
            if (drawModel != null)
            {
                return drawModel;
            }

            // Not found, now send a request to the host layer
            var request = new LotteryDrawModelRequestLast();
            drawModel = await _serviceBus.Request<LotteryDrawModelRequestLast, IDrawModelContract>(request);

            if (drawModel != null)
            {
                // Set the result back into the cache
                _cacheStore.SetObject<IDrawModelContract>(drawModel);
            }

            return drawModel;
        }

        /// <summary>
        /// Gets the specified lottery draw (GET api/lottery/draw/{id}).
        /// </summary>
        /// <returns></returns>
        [Route("draw")]
        public async Task<IDrawModelContract> Get(Guid drawReference)
        {
            // Try to get the draw data from the local database
            var drawModel = _drawStorage.FindByIdentity(drawReference);
            if (drawModel != null)
            {
                return drawModel;
            }

            // Not found, now send a request to the host layer
            var request = new LotteryDrawModelRequestById()
            {
                DrawId = drawReference
            };
            var dataContract = await _serviceBus.Request<LotteryDrawModelRequestById, IDrawModelContract>(request);

            if (dataContract != null)
            {
                drawModel = (LotteriesDrawModel)dataContract;
                _drawStorage.Save(drawModel);
            }

            return drawModel;
        }

        /// <summary>
        /// Gets the lottery draw collection (GET api/lottery/draws).
        /// </summary>
        /// <returns></returns>
        [Route("draws")]
        public async Task<IDrawCollectionContract> GetCollection(int limit = 5)
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

            var lastDraws = _drawStorage.Find(null, orderBy, limit);
            if (lastDraws != null)
            {
                return new LotteriesDrawCollection()
                {
                    Draws = lastDraws.ToArray()
                };
            }

            // Not found, now send a request to the host layer
            var request = new LotteryDrawCollectionRequestLatest()
            {
                limit = limit
            };
            var collection = await _serviceBus.Request<LotteryDrawCollectionRequestLatest, IDrawCollectionContract>(request);

            if (collection != null)
            {
                // Save the results into our local database
                foreach (var item in collection.Draws)
                    _drawStorage.Save((LotteriesDrawModel)item);

                return new LotteriesDrawCollection()
                {
                    Draws = collection.Draws.Select(x => (LotteriesDrawModel)x)
                };
            }

            return null;
        }
    }
}