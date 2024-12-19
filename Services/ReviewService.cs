using App.Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class ReviewService
    {
        private readonly ReviewRepository _repository;
        private readonly AppDbContext _context;

        public ReviewService(ReviewRepository repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public void AddReview(Review review)
        {
            //Eventuele logica of validatie toevoegen
            if (review.Rating < 1 || review.Rating > 5)
            {
                throw new ArgumentException("Rating moet tussen 1 en 5 liggen.");
            }

            // Controleer of er al een review bestaat voor deze gebruiker en track
            var existingReview = _repository.GetReviewByUserAndTrack(review.UserId, review.TrackId);

            if (existingReview != null)
            {
                throw new InvalidOperationException("De gebruiker heeft al een review voor dit nummer.");
            }

            // Voeg de review toe als er nog geen bestaat
            _repository.AddReview(review);
        }
        public List<Review> GetReviews(string trackId)
        {
            return _context.Reviews.Where(r => r.TrackId == trackId).ToList(); // Haal reviews op voor een specifiek trackId
        }

        public void DeleteReview(string trackId, int reviewId)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.TrackId == trackId && r.Id == reviewId);
            if (review == null)
            {
                throw new InvalidOperationException("Review niet gevonden.");
            }

            _repository.DeleteReview(trackId, reviewId);
        }

    }
}
