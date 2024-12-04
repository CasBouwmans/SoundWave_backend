using Microsoft.AspNetCore.Mvc;
using SoundWave_api.core;

namespace SoundWave_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchWrapper _searchWrapper;

        public SearchController(ISearchWrapper searchWrapper)
        {
            _searchWrapper = searchWrapper;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromHeader] string token, [FromQuery] string searchTerm, [FromQuery] string searchType)
        {
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Token en zoekterm zijn verplicht.");
            }

            var results = await _searchWrapper.GetSearchResults(token, searchTerm, searchType);
            return Ok(results);
        }
    }
}
