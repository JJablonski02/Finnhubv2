using Finnhubv2.FinnhubService;
using Finnhubv2.FinnhubServiceContracts;
using Finnhubv2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Finnhubv2.Controllers
{
    [Route("[controller]")]
    public class FinnhubController : Controller
    {
        private readonly TradingOptions _tradingOptions;
        private readonly IFinnHubService _finnHubService;
        private readonly IConfiguration _configuration;

        public FinnhubController(TradingOptions tradingOptions, IFinnHubService finnHubService, IConfiguration configuration)
        {
            _tradingOptions = tradingOptions;
            _finnHubService = finnHubService;
            _configuration = configuration;
        }

        [Route("/")]
        [Route("[action]")]
        [Route("~/[controller]")]
        public IActionResult Index()
        {

            if (string.IsNullOrEmpty(_tradingOptions.DefaultStockSymbol))
                _tradingOptions.DefaultStockSymbol = "MSFT";

            Dictionary<string, object>? companyProfileDictionary = _finnHubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);

            Dictionary<string, object>? stockQuoteDictionary = _finnHubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);

            Stock stock = new Stock() { stockSymbol = _tradingOptions.DefaultStockSymbol };

            if (companyProfileDictionary is not null && stockQuoteDictionary is not null)
            {
                stock = new Stock()
                {
                    stockSymbol = Convert.ToString(companyProfileDictionary["ticker"]),
                    stockName = Convert.ToString(companyProfileDictionary["name"]),
                    price = Convert.ToDouble(stockQuoteDictionary["c"].ToString())
                };
            }
            ViewBag.FinnhubToken = _configuration["FinnhubToken"];

            return View(stock);

        }
    }
}

