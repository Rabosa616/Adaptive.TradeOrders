using System.Collections.Generic;
using Adaptive.Recruitment.TradeOrders.Contracts;
using Adaptive.Recruitment.TradeOrders.System;
using FluentAssertions;
using Xunit;

namespace Adaptive.Recruitment.TradeOrders.Tests
{
    public class SellTradeOrderTests
    {
        [Theory]
        [MemberData(nameof(ReturnsExpectedResponseData))]
        public void When_Price_Is_Upper_Limit_Sell(string symbol, decimal price, decimal sellPrice, OrderStatus status)
        {
            ITradeBooker tradeBroker = new TradeBooker();
            TradeOrder testOrder = new TradeOrder(OrderType.Limit, OrderDirection.Sell, symbol, price, 50, tradeBroker);

            testOrder.OnPriceTick(symbol, sellPrice);

            testOrder.Status.Should().Be(status);

        }

        public static IEnumerable<object[]> ReturnsExpectedResponseData
        {
            get
            {
                yield return new object[] { "AAPL", 100, DecimalHelper.RandomNumberBetween(0, 99), OrderStatus.Active };
                yield return new object[] { "AAPL", 100, DecimalHelper.RandomNumberBetween(100, 200), OrderStatus.Completed };
            }
        }
    }
}
