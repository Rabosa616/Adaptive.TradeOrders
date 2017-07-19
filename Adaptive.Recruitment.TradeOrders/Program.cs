// *****************************************************************
// *****************************************************************
// *****************************************************************
// PLEASE DO NOT MODIFY THIS FILE - IT IS NOT A PART OF THE EXERCISE
// *****************************************************************
// *****************************************************************
// *****************************************************************
using System;
using Adaptive.Recruitment.TradeOrders.Contracts;
using Adaptive.Recruitment.TradeOrders.Orders;
using Adaptive.Recruitment.TradeOrders.System;

namespace Adaptive.Recruitment.TradeOrders
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var oms = new OrderManagementSystem();
            var orderFactory = new OrderFactory();

            var buyApple = orderFactory.Create(OrderType.Limit, OrderDirection.Buy, "AAPL", 100m, 50);
            var sellMicrosoft = orderFactory.Create(OrderType.Limit, OrderDirection.Sell, "MSFT", 50m, 10);

            oms.RegisterOrder(buyApple);
            oms.RegisterOrder(sellMicrosoft);

            oms.Run();

            ReadKeyboard(oms);
        }

        private static void ReadKeyboard(OrderManagementSystem oms)
        {
            Console.WriteLine("Press 'C' to cancel all orders or 'Enter' to quit.");
            var info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key == ConsoleKey.C)
                {
                    oms.CancelAllOrders();
                }
                info = Console.ReadKey(true);
            }
        }
    }
}
