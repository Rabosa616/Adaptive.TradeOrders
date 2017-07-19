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
    public sealed class TradeFailureEventArgs : EventArgs
    {
        public string FailureMessage { get; private set; }

        public TradeFailureEventArgs(string failureMessage)
        {
            FailureMessage = failureMessage;
        }
    }
}
