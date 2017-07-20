using Adaptive.Recruitment.TradeOrders.Contracts;
using Adaptive.Recruitment.TradeOrders.System;
using System.Collections.Generic;
using Xunit;

namespace Adaptive.Recruitment.TradeOrders.Tests
{
    public class BuyTradeOrderTests
    {
        // TODO - add unit tests
        [Theory]
        [MemberData(nameof(ReturnsExpectedResponseData))]
        public void When_Price_Is_Bellow_Limit_Buy(string symbol, decimal price, decimal sellPrice)
        {
            ITradeBooker tradeBroker = new TradeBooker();
            TradeOrder test = new TradeOrder(OrderType.Limit, OrderDirection.Sell, symbol, price, 50, tradeBroker);

        }

        public static IEnumerable<object[]> ReturnsExpectedResponseData
        {
            get
            {
                yield return new object[] { "AAPL", 100, DecimalHelper.RandomNumberBetween(0,200) };
                yield return new object[] { "AAPL", 100, DecimalHelper.RandomNumberBetween(0,100) };
                yield return new object[] { "AAPL", 100, DecimalHelper.RandomNumberBetween(101,200) };
            }
        }
    }
}
