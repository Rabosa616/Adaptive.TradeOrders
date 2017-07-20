using System;
using System.Linq;

namespace Adaptive.Recruitment.TradeOrders.Tests
{
    public static class StringHelper
    {
        #region Field
        private static readonly Random Random = new Random();
        #endregion

        #region Public Methods
        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWYZ0123456789";
            return Enumerable.Repeat(chars, length).Select(s => s[Random.Next(s.Length)]).ToArray()[0].ToString();
        }

        public static string UnknownRomanSymbol()
        {
            const string chars = "ABEFGHJKNOPQRSTUWYZ";
            return Enumerable.Repeat(chars, 1).Select(s => s[Random.Next(s.Length)]).ToArray()[0].ToString();
        }
        #endregion
    }
}
