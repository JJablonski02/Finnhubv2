using Finnhubv2.FinnhubServiceContracts;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Finnhubv2.FinnhubService
{
    public class FinnHubService : IFinnHubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public FinnHubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public Dictionary<string, object>? GetCompanyProfile(string stockSymbol)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration}")
            };

            HttpResponseMessage httpResponseMessage = httpClient.Send(httpRequestMessage);

            string responseBody = new StreamReader(httpResponseMessage.Content.ReadAsStream()).ReadToEnd();

            Dictionary<string,object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string,object>>(responseBody);

            if(responseDictionary == null)
            {
                throw new InvalidOperationException("No response from server");
            }
            if (responseDictionary.ContainsKey("error"))
            {
                throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));
            }
            return responseDictionary;
        }

        public Dictionary<string, object>? GetStockPriceQuote(string stockSymbol)
        {

        }

    }
}
