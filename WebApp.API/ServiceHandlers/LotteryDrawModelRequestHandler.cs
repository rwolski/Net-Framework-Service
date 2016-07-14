﻿using System.Threading.Tasks;
using WebApp.API.Models;
using WebApp.API.Contracts;
using Framework.Data;
using System;
using System.Collections.Generic;

namespace WebApp.API.ServiceHandler
{
    /// <summary>
    /// Performs the specified request
    /// </summary>
    /// <seealso cref="WebApp.API.Contracts.IDrawModelRequestByIdHandler" />
    public class LotteryDrawModelRequestHandler : IDrawModelRequestByIdHandler, IDrawModelRequestLatestHandler
    {
        readonly IEntityStorage<LotteriesDrawModel> _drawModelStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="LotteryDrawModelRequestHandler"/> class.
        /// </summary>
        public LotteryDrawModelRequestHandler(IDatabaseConnection dataConnection)
        {
            if (dataConnection == null)
                throw new ArgumentNullException("dataConnection");

            _drawModelStorage = dataConnection.GetCollection<LotteriesDrawModel>("Server_LotteriesDraw");
        }

        /// <summary>
        /// Performs the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public Task<object> Request(IDrawModelRequestById request)
        {
            var draw = _drawModelStorage.FindByIdentity(request.DrawId);
            return Task.FromResult((object)draw);
        }

        /// <summary>
        /// Performs the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public Task<object> Request(IDrawModelRequestLatest request)
        {
            var orderBy = new List<OrderBy<LotteriesDrawModel>>()
            {
                new OrderBy<LotteriesDrawModel>()
                {
                    Exp = x => x.Id,
                    Ascending = false
                }
            };

            var lastDraw = _drawModelStorage.FindFirstOrDefault(null, orderBy);
            if (lastDraw != null)
            {
                //lastDraw.Id = null;
                return Task.FromResult((object)lastDraw);
            }

            return Task.FromResult<object>(null);
        }
    }
}