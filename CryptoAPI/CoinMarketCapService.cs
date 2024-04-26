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
            try
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
                    return jsonResponse;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
                return null;
            }
        }

        // Returns a cryptocurrency based on user input
        public async Task<string> GetCryptoPriceAsync(string slug)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"v1/cryptocurrency/quotes/latest?slug={slug}");
                var client = _clientFactory.CreateClient("CoinMarketCapClient");

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return jsonResponse;
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Error: {errorResponse}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
                return null;
            }
        }
    }
}
