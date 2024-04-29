namespace CryptoAPI.Models
{
    public class SingleCryptoResponse
    {
        public Status Status { get; set; }
        public Dictionary<string, CryptoData> Data { get; set; }
    }

    public class Status
    {
        public DateTime Timestamp { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public int Elapsed { get; set; }
        public int CreditCount { get; set; }
        public string Notice { get; set; }
    }

    public class CryptoData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public Quote Quote { get; set; }
    }

    public class Quote
    {
        public Currency USD { get; set; }
    }

    public class Currency
    {
        public decimal Price { get; set; }
        public decimal MarketCap { get; set; }
    }

    public class CryptoInfo
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal? Price { get; set; }
        public decimal? MarketCap { get; set; }
        // Add more properties as needed
    }

}
