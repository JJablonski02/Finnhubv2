namespace Finnhubv2.FinnhubServiceContracts
{
    public interface IFinnHubService
    {
        Dictionary<string, object>? GetCompanyProfile(string stockSymbol);
        Dictionary<string,object>? GetStockPriceQuote(string stockSymbol);
    }
}
