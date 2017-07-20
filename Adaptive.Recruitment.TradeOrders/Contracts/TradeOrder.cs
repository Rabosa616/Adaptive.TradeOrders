// *****************************************************************
// *****************************************************************
// *****************************************************************
// PLEASE DO NOT MODIFY THIS FILE - IT IS NOT A PART OF THE EXERCISE
// *****************************************************************
// *****************************************************************
// *****************************************************************
using Adaptive.Recruitment.TradeOrders.System;
using System;
using System.Diagnostics;

namespace Adaptive.Recruitment.TradeOrders.Contracts
{
    public class TradeOrder : ITradeOrder
    {
        #region Fields
        private decimal _price;
        private int _volume;
        private ITradeBooker _tradeBooker;
        private static readonly object padlock = new object();
        #endregion

        #region Properties
        public string Symbol { get; private set; }

        public OrderDirection Direction { get; private set; }

        public OrderType Type { get; private set; }

        public OrderStatus Status { get; private set; }
        #endregion

        #region Events
        public event EventHandler<TradeFailureEventArgs> TradeFailure;
        public event EventHandler TradeSuccess;
        #endregion

        #region Constructor
        public TradeOrder(OrderType type, OrderDirection direction, string symbol, decimal price, int volume, ITradeBooker tradeBooker)
        {
            Symbol = symbol;
            Direction = direction;
            Type = type;
            Status = OrderStatus.Active;
            _volume = volume;
            _price = price;
            _tradeBooker = tradeBooker;
            TradeSuccess = (s, e) => { return; };
            TradeFailure = (s, e) => { return; };
        }
        #endregion

        #region Public Methods
        public void Cancel()
        {
            Status = OrderStatus.Cancelled;
        }

        public void OnPriceTick(string stockSymbol, decimal price)
        {
            if (Symbol.Equals(stockSymbol) && Status == OrderStatus.Active)
            {
                lock (padlock)
                {
                    if (Status == OrderStatus.Active )
                    {
                        try
                        {
                            ExecuteOrder(stockSymbol, price);
                        }
                        catch (Exception ex)
                        {
                            Status = OrderStatus.Failed;
                            OnTradeFailure(new TradeFailureEventArgs(ex.Message));
                        }
                    }
                }
            }
        }
        #endregion

        #region Private/Protected methods
        private void ExecuteOrder(string stockSymbol, decimal price)
        {
            if (Direction == OrderDirection.Buy && price <= _price)
            {
                Buy(stockSymbol, price);
            }
            if (Direction == OrderDirection.Sell && price >= _price)
            {
                Sell(stockSymbol, price);
            }
        }

        private void Sell(string stockSymbol, decimal price)
        {
            _tradeBooker.Sell(stockSymbol, _volume, price);
            Status = OrderStatus.Completed;
            OnTradeSuccess();
        }

        private void Buy(string stockSymbol, decimal price)
        {
            _tradeBooker.Buy(stockSymbol, _volume, price);
            Status = OrderStatus.Completed;
            OnTradeSuccess();
        }

        protected void OnTradeSuccess()
        {
            EventHandler handler = TradeSuccess;
            if (handler != null)
            {
                handler(this, null);
            }
        }

        protected void OnTradeFailure(TradeFailureEventArgs e)
        {
            EventHandler<TradeFailureEventArgs> handler = TradeFailure;
            handler?.Invoke(this, e);
        }
        #endregion
    }
}