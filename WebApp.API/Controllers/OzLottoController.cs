using Framework.Data;
using Framework.Queue;
using System.Linq;
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
            var mongoProvider = Configuration.DependencyResolver.GetService(typeof(IDatabaseProvider)) as MongoDatabaseProvider;

            var con = mongoProvider.GetDatabase<OzLottoDrawModel>() as MongoDatabaseConnection;
            var database = new MongoEntityStorage<OzLottoDrawModel>(con);
            var drawModel = database.FindAll().FirstOrDefault();

            if (drawModel != null)
                return drawModel;

            var queueProvider = Configuration.DependencyResolver.GetService(typeof(IQueueProvider)) as RabbitMQProvider;
            using (var q = queueProvider.GetQueue<OzLottoDrawModel>())
            {
                drawModel = q.Receive<OzLottoDrawModel>();

                if (drawModel != null)
                    database.Save(drawModel);

                return drawModel;
            }
        }
    }
}
