using System.Collections.Generic;
using Adaptive.Recruitment.TradeOrders.Contracts;
using Adaptive.Recruitment.TradeOrders.System;
using FluentAssertions;
using Xunit;

namespace Adaptive.Recruitment.TradeOrders.Tests
{
    public class CancelTradeOrderTest
    {
        [Theory]
        [MemberData(nameof(ReturnsExpectedResponseData))]
        public void When_Cancel_Status_Shoud_Be_Cancelled(string symbol, decimal price, decimal sellPrice, OrderStatus status)
        {
            ITradeBooker tradeBroker = new TradeBooker();
            TradeOrder testOrder = new TradeOrder(OrderType.Limit, OrderDirection.Buy, symbol, price, 50, tradeBroker);

            testOrder.Cancel();

            testOrder.Status.Should().Be(status);
        }

        public static IEnumerable<object[]> ReturnsExpectedResponseData
        {
            get
            {
                yield return new object[] { "AAPL", 100, DecimalHelper.RandomNumberBetween(0, 100), OrderStatus.Cancelled };
                yield return new object[] { "AAPL", 100, DecimalHelper.RandomNumberBetween(101, 200), OrderStatus.Cancelled };
            }
        }
    }
}