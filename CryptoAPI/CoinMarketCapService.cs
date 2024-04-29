using CryptoAPI.Models;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        //Returns a cryptocurrency based on user input
        public async Task<CryptoInfo> GetCryptoInfoAsync(string slug)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"v1/cryptocurrency/quotes/latest?slug={slug}");
                var client = _clientFactory.CreateClient("CoinMarketCapClient");

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Adjust Serializer options because JSON and C# naming conventions are different
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        AllowTrailingCommas = true
                    };

                    // Deserializes the JSON response into model SingleCryptoResponse
                    var result = JsonSerializer.Deserialize<SingleCryptoResponse>(jsonResponse, options);

                    if (result?.Data != null)
                    {
                        // Extract the data from SingleCryptoResponse.Data
                        var cryptoData = result.Data.Values.FirstOrDefault();
                        if (cryptoData != null)
                        {
                            return new CryptoInfo
                            {
                                Name = cryptoData.Name,
                                Symbol = cryptoData.Symbol,
                                Price = cryptoData.Quote.USD.Price,
                                MarketCap = cryptoData.Quote.USD.MarketCap
                            };
                        }
                    }
                    return new CryptoInfo { Name = "Not found", Price = null };
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Error: {errorResponse}");
                    return new CryptoInfo { Name = "Error", Price = null };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
                return new CryptoInfo { Name = "Exception", Price = null };
            }
        }
    }
}
