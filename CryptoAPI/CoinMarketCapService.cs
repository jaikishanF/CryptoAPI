namespace CryptoAPI
{
    public class CoinMarketCapService
    {
        private readonly IHttpClientFactory _clientFactory;

        // The factory is used to create HttpClient instances
        public CoinMarketCapService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // Returns a list of all active cryptocurrencies with latest market data
        public async Task<string> GetLatestListingsAsync()
        {
            // Setup the endpoint
            var request = new HttpRequestMessage(HttpMethod.Get, "v1/cryptocurrency/listings/latest");
            // Create instance of the configured Http client
            var client = _clientFactory.CreateClient("CoinMarketCapClient");

            // Send the request
            HttpResponseMessage response = await client.SendAsync(request);
            
            // If succesfull request return it
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return jsonResponse;  // Or deserialize with JsonConvert.DeserializeObject if needed
            }
            else
            {
                return null;
            }
        }
    }
}
