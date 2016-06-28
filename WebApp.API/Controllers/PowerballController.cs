using Framework.Cache;
using Framework.Queue;
using System.Web.Http;
using WebApp.API.Models;

namespace WebApp.API.Controllers
{
    /// <summary>
    /// Powerball draw controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class PowerballController : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PowerballController"/> class.
        /// </summary>
        public PowerballController()
        {
            //RabbitMQProvider provider = new RabbitMQProvider(ConfigurationManager.AppSettings["RabbitMQHostname"]);
            //provider.AddQueueSub();
            //using (var q = provider.GetQueue("Powerball"))
            //{
            //    q.AddConsumer();
            //}
        }


        /// <summary>
        /// Gets the latest powerball draw (GET api/powerball).
        /// </summary>
        /// <returns></returns>
        public PowerballDrawModel Get()
        {
            var redisProvider = Configuration.DependencyResolver.GetService(typeof(ICacheProvider)) as RedisProvider;

            var store = redisProvider.GetStore(0);
            var drawModel = store.GetObject<PowerballDrawModel>();

            if (drawModel != null)
            {
                return drawModel;
            }

            var queueProvider = Configuration.DependencyResolver.GetService(typeof(IQueueProvider)) as RabbitMQProvider;
            using (var q = queueProvider.GetQueue<PowerballDrawModel>())
            {
                drawModel = q.Receive<PowerballDrawModel>();
                if (drawModel != null)
                    store.SetObject(drawModel);

                return drawModel;
            }
        }
    }
}