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
    }
}
