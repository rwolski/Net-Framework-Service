using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.API.Models;

namespace WebApp.API.Controllers
{
    /// <summary>
    /// Oz lotto controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class OzLottoController : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OzLottoController"/> class.
        /// </summary>
        public OzLottoController()
        {
        }


        /// <summary>
        /// Gets the latest powerball draw (GET api/powerball).
        /// </summary>
        /// <returns></returns>
        public OzLottoDrawModel Get()
        {
            var redisProvider = Configuration.DependencyResolver.GetService(typeof(ICacheProvider)) as RedisProvider;

            var store = redisProvider.GetStore(0);

            var drawModel = store.GetObject<OzLottoDrawModel>();

            if (drawModel != null)
            {
                return drawModel;
            }

            var queueProvider = Configuration.DependencyResolver.GetService(typeof(IQueueProvider)) as RabbitMQProvider;
            using (var q = queueProvider.GetQueue("Powerball"))
            {
                drawModel = q.Receive<OzLottoDrawModel>();
                store.SetObject("Powerball", drawModel);

                return drawModel;
            }
        }
    }
}
