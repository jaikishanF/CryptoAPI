using Microsoft.AspNetCore.Mvc;

namespace CryptoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly CoinMarketCapService _cmcService;

        // Setup instance to work with the external API
        public CryptoController(CoinMarketCapService cmcService)
        {
            _cmcService = cmcService;
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestListings()
        {
            var listings = await _cmcService.GetLatestListingsAsync();
            return Ok(listings);
        }

        // Get cryptocurrency based on user input
        [HttpGet("details/{slug}")]
        public async Task<IActionResult> GetCryptoPrice(string slug)
        {
            var data = await _cmcService.GetCryptoInfoAsync(slug);
            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return NotFound($"Information for the slug '{slug}' is not available.");
            }
        }
    }
}
