using Adaptive.Recruitment.TradeOrders.Contracts;
using Adaptive.Recruitment.TradeOrders.System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Adaptive.Recruitment.TradeOrders.Tests
{
    public class BuyTradeOrderTests
    {
        [Theory]
        [MemberData(nameof(ReturnsExpectedResponseData))]
        public void When_Price_Is_Bellow_Limit_Buy(string symbol, decimal price, decimal sellPrice, OrderStatus status)
        {
            ITradeBooker tradeBroker = new TradeBooker();
            TradeOrder testOrder = new TradeOrder(OrderType.Limit, OrderDirection.Buy, symbol, price, 50, tradeBroker);

            testOrder.OnPriceTick(symbol,sellPrice);

            testOrder.Status.Should().Be(status);

        }

        public static IEnumerable<object[]> ReturnsExpectedResponseData
        {
            get
            {
                yield return new object[] { "AAPL", 100, DecimalHelper.RandomNumberBetween(0,100), OrderStatus.Completed };
                yield return new object[] { "AAPL", 100, DecimalHelper.RandomNumberBetween(101,200), OrderStatus.Active };
            }
        }
    }
}
