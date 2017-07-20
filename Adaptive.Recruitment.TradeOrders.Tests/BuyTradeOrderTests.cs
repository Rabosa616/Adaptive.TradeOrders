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
        public void When_Price_Is_Bellow_Limit_Sell(string symbol, decimal price, decimal sellPrice, OrderStatus status)
        {
            ITradeBooker tradeBroker = new TradeBooker();
            TradeOrder testOrder = new TradeOrder(OrderType.Limit, OrderDirection.Buy, symbol, price, 50, tradeBroker);

            testOrder.OnPriceTick(symbol, sellPrice);
            testOrder.Status.Should().Be(status);

        }

        [Theory]
        [MemberData(nameof(ReturnsExpectedFailingResponseData))]
        public void When_Price_After_Divide_By_2_Shoud_Have_Status_Failed(string symbol, decimal price, decimal sellPrice, OrderStatus status)
        {
            ITradeBooker tradeBroker = new TradeBooker();
            TradeOrder testOrder = new TradeOrder(OrderType.Limit, OrderDirection.Buy, symbol, price, 50, tradeBroker);

            testOrder.OnPriceTick(symbol, sellPrice);
            testOrder.Status.Should().Be(status);

        }

        public static IEnumerable<object[]> ReturnsExpectedResponseData
        {
            get
            {
                yield return new object[] { "AAPL", 100, DecimalHelper.RandomNumberBetween(0, 100), OrderStatus.Completed };
                yield return new object[] { "AAPL", 100, DecimalHelper.RandomNumberBetween(101, 200), OrderStatus.Active };
                yield return new object[] { "AAPL", 100, DecimalHelper.RandomNumberBetween(101, 200), OrderStatus.Active };
            }
        }

        public static IEnumerable<object[]> ReturnsExpectedFailingResponseData
        {
            get
            {
                yield return new object[] { "AAPL", 100, DecimalHelper.RandomReminderTwoNumberIsCeroBetween(0, 100), OrderStatus.Failed };
                yield return new object[] { "AAPL", 100, DecimalHelper.RandomReminderTwoNumberIsCeroBetween(0, 100), OrderStatus.Failed };
                yield return new object[] { "AAPL", 100, DecimalHelper.RandomReminderTwoNumberIsCeroBetween(0, 100), OrderStatus.Failed };
            }
        }
    }
}
