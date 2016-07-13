using Framework.Core;
using Framework.ServiceBus;
using System;

namespace WebApp.API.Contracts
{
    public interface IDrawModelContract : IMessageContract, IAuditable
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
