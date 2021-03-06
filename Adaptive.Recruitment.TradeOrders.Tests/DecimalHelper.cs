﻿using System;

namespace Adaptive.Recruitment.TradeOrders.Tests
{
    public static class DecimalHelper
    {
        private static readonly Random random = new Random();

        public static decimal RandomNumberBetween(decimal minValue, decimal maxValue)
        {
            decimal next = Convert.ToDecimal(random.NextDouble());
            return minValue + (next * (maxValue - minValue));
        }

        public static decimal RandomReminderTwoNumberIsCeroBetween(int minValue, int maxValue)
        {
            decimal result = random.Next(minValue, maxValue);
            while (result % 2 != 0)
            {
                result = random.Next(minValue, maxValue);
            }
            return result;
        }
    }
}
