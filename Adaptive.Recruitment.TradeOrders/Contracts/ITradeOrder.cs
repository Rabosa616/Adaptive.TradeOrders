// *****************************************************************
// *****************************************************************
// *****************************************************************
// PLEASE DO NOT MODIFY THIS FILE - IT IS NOT A PART OF THE EXERCISE
// *****************************************************************
// *****************************************************************
// *****************************************************************
using System;

namespace Adaptive.Recruitment.TradeOrders.Contracts
{
    public interface ITradeOrder
    {
        string Symbol { get; }
        OrderDirection Direction { get; }
        OrderType Type { get; }
        OrderStatus Status { get; }

        event EventHandler<TradeFailureEventArgs> TradeFailure;
        event EventHandler TradeSuccess;

        void OnPriceTick(string stockSymbol, decimal price);
        void Cancel();
    }
}
