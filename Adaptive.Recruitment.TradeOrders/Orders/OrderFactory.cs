using System;
using Adaptive.Recruitment.TradeOrders.Contracts;
using Adaptive.Recruitment.TradeOrders.System;

namespace Adaptive.Recruitment.TradeOrders.Orders
{
    public class OrderFactory
    {
        private readonly ITradeBooker _tradeBooker;

        public OrderFactory()
        {
            _tradeBooker = new TradeBooker();
        }

        public ITradeOrder Create(OrderType type, OrderDirection direction, string symbol, decimal price, int volume)
        {
            switch (type)
            {
                case OrderType.Limit:
                    return new TradeOrder(type, direction, symbol, price, volume, _tradeBooker);
                case OrderType.Market:
                case OrderType.Stop:
                case OrderType.TrailingStop:
                case OrderType.StopLimit:
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
