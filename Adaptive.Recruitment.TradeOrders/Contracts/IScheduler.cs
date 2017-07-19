using System;

namespace Adaptive.Recruitment.TradeOrders.Contracts
{
    /// <summary>
    /// Interface for scheduling work items onto different threads.
    /// </summary>
    public interface IScheduler
    {
        void Run(Action action);
    }
}
