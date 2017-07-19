// *****************************************************************
// *****************************************************************
// *****************************************************************
// PLEASE DO NOT MODIFY THIS FILE - IT IS NOT A PART OF THE EXERCISE
// *****************************************************************
// *****************************************************************
// *****************************************************************
namespace Adaptive.Recruitment.TradeOrders.Contracts
{
    public interface ITradeBooker
    {
        void Buy(string stockSymbol, int volume, decimal price);
        void Sell(string stockSymbol, int volume, decimal price);
    }
}
