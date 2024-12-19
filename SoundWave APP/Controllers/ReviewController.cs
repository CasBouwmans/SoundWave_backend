using Domain;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace SoundWave_APP.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public IActionResult AddReview([FromBody] Review review)
        {
            try
            {
                _reviewService.AddReview(review);
                return Ok("Review toegevoegd!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{trackId}")]
        public IActionResult GetReviews(string trackId)
        {
            // Haal de reviews op voor de specifieke trackId via de service
            var reviews = _reviewService.GetReviews(trackId);

            if (reviews == null || !reviews.Any())
            {
                return NotFound("Geen reviews gevonden.");
            }

            return Ok(reviews); // Stuur de reviews terug als response
        }

        [HttpDelete("{trackId}/{reviewId}")]
        public IActionResult DeleteReview(string trackId, int reviewId)
        {
            try
            {
                _reviewService.DeleteReview(trackId, reviewId);
                return Ok("Review verwijderd!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

