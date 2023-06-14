namespace Finnhubv2.Models
{
    public class Stock
    {
        public string? stockName { get; set; }
        public string? stockSymbol { get; set; }
        public double price { get; set; } = 0;
        public double quantity { get; set; } = 0;
    }
}
