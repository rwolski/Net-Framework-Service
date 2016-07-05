using Autofac;
using Framework.Data;
using Framework.Queue;
using System;
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
        readonly IDatabaseConnection _dataConnection;
        readonly IQueueProvider _queueProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="OzLottoController"/> class.
        /// </summary>
        public OzLottoController(IDatabaseConnection dataConnection, IQueueProvider queueProvider)
        {
            if (dataConnection == null)
                throw new ArgumentNullException("dataConnection");
            if (queueProvider == null)
                throw new ArgumentNullException("queueProvider");

            _queueProvider = queueProvider;
            _dataConnection = dataConnection;
        }


        /// <summary>
        /// Gets the latest powerball draw (GET api/powerball).
        /// </summary>
        /// <returns></returns>
        public OzLottoDrawModel Get()
        {
            //var store = _dataConnection.GetCollection<OzLottoDrawModel>();

            //// Get the last closed draw
            //var drawModel = store.FindFirstOrDefault(
            //    new WhereCondition<OzLottoDrawModel>(x => x.DrawStatus == DrawStatusCode.Closed),
            //    new List<OrderBy<OzLottoDrawModel>>()
            //    {
            //        new OrderBy<OzLottoDrawModel>()
            //        {
            //            Exp = x => x.DrawNumber,
            //            Ascending = false
            //        }
            //    });

            //if (drawModel != null)
            //{
            //    return drawModel;
            //}

            //using (var q = _queueProvider.GetQueue<OzLottoDrawModel>())
            //{
            //    drawModel = q.Receive().Result;
            //    if (drawModel != null)
            //        store.Save(drawModel);

            //    return drawModel;
            //}

            return new OzLottoDrawModel();
        }
    }
}
