using Autofac;
using Framework.Data;
using Framework.Queue;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApp.API.Models;

namespace WebApp.API.Controllers
{
    /// <summary>
    /// Oz lotto controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/ozlotto")]
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
            using (var scope = this.BeginScope())
            {
                var dataProvider = scope.Resolve<IDatabaseProvider>();
                var database = dataProvider.GetDatabase();
                var store = database.GetCollection<OzLottoDrawModel>();

                // Get the last closed draw
                var drawModel = store.FindFirstOrDefault(
                    new WhereCondition<OzLottoDrawModel>(x => x.DrawStatus == DrawStatusCode.Closed),
                    new List<OrderBy<OzLottoDrawModel>>()
                    {
                        new OrderBy<OzLottoDrawModel>()
                        {
                            Exp = x => x.DrawNumber,
                            Ascending = false
                        }
                    });

                if (drawModel != null)
                {
                    return drawModel;
                }

                var queueProvider = scope.Resolve<IQueueProvider>();
                using (var q = queueProvider.GetQueue<OzLottoDrawModel>())
                {
                    drawModel = q.Receive<OzLottoDrawModel>();
                    if (drawModel != null)
                        store.Save(drawModel);

                    return drawModel;
                }
            }
        }
    }
}
