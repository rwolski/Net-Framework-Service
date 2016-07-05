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
    [RoutePrefix("api/powerball")]
    public class PowerballController : ApiController
    {
        readonly ICacheProvider _cacheProvider;
        readonly IQueueProvider _queueProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerballController"/> class.
        /// </summary>
        public PowerballController(ICacheProvider cacheProvider, IQueueProvider queueProvider)
        {
            if (cacheProvider == null)
                throw new ArgumentNullException("cacheProvider");
            if (queueProvider == null)
                throw new ArgumentNullException("queueProvider");

            _cacheProvider = cacheProvider;
            _queueProvider = queueProvider;
        }


        /// <summary>
        /// Gets the latest powerball draw (GET api/powerball).
        /// </summary>
        /// <returns></returns>
        public PowerballDrawModel Get()
        {
            //var store = _cacheProvider.GetStore(AppSettings.RedisDatabaseIndex);
            //var drawModel = store.GetObject<PowerballDrawModel>();

            //if (drawModel != null)
            //{
            //    return drawModel;
            //}

            //using (var q = _queueProvider.GetQueue<PowerballDrawModel>())
            //{
            //    drawModel = q.Receive().Result;
            //    if (drawModel != null)
            //        store.SetObject(drawModel);

            //    return drawModel;
            //}

            return new PowerballDrawModel();
        }
    }
}