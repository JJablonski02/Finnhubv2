namespace Finnhubv2.FinnhubServiceContracts
{
    public interface IFinnHubService
    {
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);
        Task<Dictionary<string,object>?> GetStockPriceQuote(string stockSymbol);
    }
}
