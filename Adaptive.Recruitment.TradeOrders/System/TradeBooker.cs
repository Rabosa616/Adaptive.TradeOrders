// *****************************************************************
// *****************************************************************
// *****************************************************************
// PLEASE DO NOT MODIFY THIS FILE - IT IS NOT A PART OF THE EXERCISE
// *****************************************************************
// *****************************************************************
// *****************************************************************
using System;
using System.Threading;
using Adaptive.Recruitment.TradeOrders.Contracts;

namespace Adaptive.Recruitment.TradeOrders.System
{
    /// <summary>
    /// This is an implementation of ITradeBooker that we have been provided by another team and cannot modify.
    /// </summary>
    public class TradeBooker : ITradeBooker
    {
        public void Buy(string stockSymbol, int volume, decimal price)
        {
            // simulating a long running call
            Thread.Sleep(2000);

            if (price % 2 == 0)
            {
                throw new Exception(string.Format("Unknown error occurred while buying stock '{0}'", stockSymbol));
            }
        }

        public void Sell(string stockSymbol, int volume, decimal price)
        {
            // simulating a long running call
            Thread.Sleep(2000);

            if (price % 2 == 0)
            {
                throw new Exception(string.Format("Unknown error occurred while selling stock '{0}'", stockSymbol));
            }
        }
    }
}
