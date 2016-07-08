using Framework.Core;
using Framework.ServiceBus;
using System;

namespace WebApp.API
{
    /// <summary>
    /// Interface for transit draw events
    /// </summary>
    /// <seealso cref="Framework.ServiceBus.IServiceEvent" />
    /// <seealso cref="Framework.Core.IAuditable" />
    public interface ITransitLottoDrawEvent : IServiceEvent, IAuditable
    {
        /// <summary>
        /// Gets the draw winning numbers.
        /// </summary>
        /// <value>
        /// The draw winning numbers.
        /// </value>
        int[] DrawWinningNumbers { get; }

        /// <summary>
        /// Gets the draw date time.
        /// </summary>
        /// <value>
        /// The draw date time.
        /// </value>
        DateTime DrawDateTime { get; }

        /// <summary>
        /// Gets the draw number.
        /// </summary>
        /// <value>
        /// The draw number.
        /// </value>
        int DrawNumber { get; }

        /// <summary>
        /// Gets the draw status.
        /// </summary>
        /// <value>
        /// The draw status.
        /// </value>
        DrawStatusCode DrawStatus { get; }
    }
}