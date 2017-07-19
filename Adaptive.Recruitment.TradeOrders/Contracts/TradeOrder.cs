// *****************************************************************
// *****************************************************************
// *****************************************************************
// PLEASE DO NOT MODIFY THIS FILE - IT IS NOT A PART OF THE EXERCISE
// *****************************************************************
// *****************************************************************
// *****************************************************************
using System;
using System.Diagnostics;

namespace Adaptive.Recruitment.TradeOrders.Contracts
{
    public class TradeOrder : ITradeOrder
    {
        private decimal _price;
        private int _volume;
        private ITradeBooker _tradeBooker;
        public string Symbol { get; private set; }

        public OrderDirection Direction { get; private set; }

        public OrderType Type { get; private set; }

        public OrderStatus Status { get; private set; }

        public event EventHandler<TradeFailureEventArgs> TradeFailure;
        public event EventHandler TradeSuccess;

        public TradeOrder(OrderType type, OrderDirection direction, string symbol, decimal price, int volume, ITradeBooker tradeBooker)
        {
            Symbol = symbol;
            Direction = direction;
            Type = type;
            Status = OrderStatus.Active;
            _volume = volume;
            _price = price;
            _tradeBooker = tradeBooker;
        }

        public void Cancel()
        {
            Status = OrderStatus.Cancelled;
        }

        public void OnPriceTick(string stockSymbol, decimal price)
        {
            lock (_tradeBooker)
            {
                if (Symbol.Equals(stockSymbol))
                {

                    if (Status == OrderStatus.Active)
                    {
                        try
                        {
                            if (Direction == OrderDirection.Buy && price <= _price)
                            {
                                _tradeBooker.Buy(stockSymbol, _volume, price);
                                Status = OrderStatus.Completed;
                                OnTradeSuccess();
                            }
                            if (Direction == OrderDirection.Sell && price >= _price)
                            {
                                _tradeBooker.Sell(stockSymbol, _volume, price);
                                Status = OrderStatus.Completed;
                                OnTradeSuccess();
                            }
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
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }

}
