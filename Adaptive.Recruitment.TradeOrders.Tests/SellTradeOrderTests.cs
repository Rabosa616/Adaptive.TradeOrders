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

        [Theory]
        [MemberData(nameof(ReturnsExpectedFailingResponseData))]
        public void When_Price_After_Divide_By_2_Shoud_Have_Status_Failed(string symbol, decimal price, decimal sellPrice, OrderStatus status)
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
                yield return new object[] { "MSFT", 50, DecimalHelper.RandomNumberBetween(0, 49), OrderStatus.Active };
                yield return new object[] { "MSFT", 50, DecimalHelper.RandomNumberBetween(50, 200), OrderStatus.Completed };
            }
        }

        public static IEnumerable<object[]> ReturnsExpectedFailingResponseData
        {
            get
            {
                yield return new object[] { "MSFT", 50, DecimalHelper.RandomReminderTwoNumberIsCeroBetween(50, 200), OrderStatus.Failed };
                yield return new object[] { "MSFT", 50, DecimalHelper.RandomReminderTwoNumberIsCeroBetween(50, 200), OrderStatus.Failed };
                yield return new object[] { "MSFT", 50, DecimalHelper.RandomReminderTwoNumberIsCeroBetween(50, 200), OrderStatus.Failed };
            }
        }
    }
}
