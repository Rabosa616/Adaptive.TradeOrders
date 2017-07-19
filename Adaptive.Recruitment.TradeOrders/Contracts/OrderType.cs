// *****************************************************************
// *****************************************************************
// *****************************************************************
// PLEASE DO NOT MODIFY THIS FILE - IT IS NOT A PART OF THE EXERCISE
// *****************************************************************
// *****************************************************************
// *****************************************************************
namespace Adaptive.Recruitment.TradeOrders.Contracts
{
    public enum OrderType
    {
        /// <summary>
        /// A limit order triggers when a minimum specified threshold has been breached in favour of the client.
        /// </summary>
        Limit,

        /// <summary>
        /// A limit order triggers immediately at the current market price.
        /// </summary>
        Market,

        /// <summary>
        /// A stop order triggers when a maximum specified threshold has been breached against the client, to limit losses.
        /// </summary>
        Stop,

        /// <summary>
        /// A trailing stop behaves like a Stop order except the threshold dynamically adjusts.
        /// </summary>
        TrailingStop,

        /// <summary>
        /// A stop limit order combines the features of a stop order with those of a limit order.
        /// </summary>
        StopLimit
    }
}
