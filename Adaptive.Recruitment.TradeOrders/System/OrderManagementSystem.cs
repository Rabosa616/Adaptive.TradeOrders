// *****************************************************************
// *****************************************************************
// *****************************************************************
// PLEASE DO NOT MODIFY THIS FILE - IT IS NOT A PART OF THE EXERCISE
// *****************************************************************
// *****************************************************************
// *****************************************************************
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Adaptive.Recruitment.TradeOrders.Contracts;

namespace Adaptive.Recruitment.TradeOrders.System
{
    internal class OrderManagementSystem
    {
        private readonly ConcurrentBag<ITradeOrder> _registeredOrders = new ConcurrentBag<ITradeOrder>();

        private readonly Tuple<string, decimal>[] _supportedSymbols =
        {
            new Tuple<string, decimal>("AAPL", 90m),
            new Tuple<string, decimal>("MSFT", 45m),
            new Tuple<string, decimal>("GOOG", 700m),
            new Tuple<string, decimal>("AMZN", 550m),
            new Tuple<string, decimal>("NFLX", 100m),
            new Tuple<string, decimal>("FB", 100m),
        };

        public void RegisterOrder(ITradeOrder tradeOrder)
        {
            tradeOrder.TradeSuccess += (s, e) => Console.WriteLine("*** Trade Success from order '{0}' ***", BuildStringFromOrder(tradeOrder));
            tradeOrder.TradeFailure += (s, e) => Console.WriteLine("*** Trade Failure from order '{0}' - '{1}' ***", BuildStringFromOrder(tradeOrder), e.FailureMessage);
            _registeredOrders.Add(tradeOrder);
        }

        public Task Run()
        {
            Console.WriteLine("Starting...");

            var tasks = Enumerable.Range(0, 100).Select(_ =>
             {
                 return Task.Factory.StartNew(() =>
                 {
                     var r = new Random();
                     while (true)
                     {
                         var symbolForPriceUpdate = _supportedSymbols[r.Next(_supportedSymbols.Length)];
                         var priceUpdate = symbolForPriceUpdate.Item2 + r.Next(10);

                         foreach (var registeredOrder in _registeredOrders)
                         {
                             var callTime = DateTime.UtcNow;

                             try
                             {
                                 registeredOrder.OnPriceTick(symbolForPriceUpdate.Item1, priceUpdate);
                             }
                             catch (Exception)
                             {
                                 Console.WriteLine("FATAL ERROR - OnPriceTick to order '{0}' threw an exception.", BuildStringFromOrder(registeredOrder));
                                 throw;
                             }

                             if (DateTime.UtcNow > callTime.AddSeconds(1))
                             {
                                 Console.WriteLine("WARNING - OnPriceTick to order '{0}' took over 1 second.", BuildStringFromOrder(registeredOrder));
                             }
                         }

                         Thread.Sleep(1000);
                     }
                 }, TaskCreationOptions.LongRunning);
             }).ToArray();

            Console.WriteLine("Started.");

            return Task.WhenAll(tasks);
        }

        public void CancelAllOrders()
        {
            if (_registeredOrders.Any())
            {
                Console.WriteLine("Cancelling {0} orders.", _registeredOrders.Count);
                foreach (var registeredOrder in _registeredOrders)
                {
                    registeredOrder.Cancel();
                }
            }
            else
            {
                Console.WriteLine("No running orders.");
            }
        }

        private string BuildStringFromOrder(ITradeOrder order)
        {
            return string.Format("{0} {1} order ({2} - {3})", order.Direction, order.Type, order.Symbol, order.Status);
        }
    }
}