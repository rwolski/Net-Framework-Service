using Framework.Data;
using Framework.Queue;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApp.API.Contracts;
using WebApp.API.Models;

namespace WebApp.API.Controllers
{
    /// <summary>
    /// Mongo and rabbit test controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/mongotest")]
    public class MongoTestController : ApiController
    {
        readonly IDatabaseConnection _dataConnection;
        readonly ISimpleQueueProvider _queueProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoTestController"/> class.
        /// </summary>
        public MongoTestController(IDatabaseConnection dataConnection, ISimpleQueueProvider queueProvider)
        {
            if (dataConnection == null)
                throw new ArgumentNullException("dataConnection");
            if (queueProvider == null)
                throw new ArgumentNullException("queueProvider");

            _queueProvider = queueProvider;
            _dataConnection = dataConnection;
        }


        /// <summary>
        /// Gets the latest model (GET api/mongotest).
        /// </summary>
        /// <returns></returns>
        public MongoTestModel Get()
        {
            var store = _dataConnection.GetCollection<MongoTestModel>();

            // Get the last model
            var model = store.FindFirstOrDefault(
                new WhereCondition<MongoTestModel>(x => x.Status == MongoStatusCode.Closed),
                new List<OrderBy<MongoTestModel>>()
                {
                    new OrderBy<MongoTestModel>()
                    {
                        Exp = x => x.Number,
                        Ascending = false
                    }
                });

            if (model != null)
            {
                return model;
            }

            using (var q = _queueProvider.GetQueue<MongoTestModel>())
            {
                model = q.Receive().Result;
                if (model != null)
                    store.Save(model);

                return model;
            }
        }
    }
}
