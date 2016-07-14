using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.API.Contracts;

namespace WebApp.API
{
    /// <summary>
    /// Collection of lottery draw models
    /// </summary>
    /// <seealso cref="WebApp.API.Contracts.IDrawCollectionContract" />
    public class LotteriesDrawCollection : IDrawCollectionContract
    {
        /// <summary>
        /// Gets the draws.
        /// </summary>
        /// <value>
        /// The draws.
        /// </value>
        public IEnumerable<IDrawModelContract> Draws { get; set; }
    }
}