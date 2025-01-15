using App.Data;
using Domain;

namespace Services
{
    public class ReviewService
    {
        private readonly ReviewRepository _repository;

        public ReviewService(ReviewRepository repository)
        {
            _repository = repository;
        }

        public void AddReview(Review review)
        {
            if (review.Rating < 1 || review.Rating > 5)
            {
                throw new ArgumentException("Rating moet tussen 1 en 5 liggen.");
            }

            var existingReview = _repository.GetReviewByUserAndTrack(review.UserId, review.TrackId);

            if (existingReview != null)
            {
                throw new InvalidOperationException("De gebruiker heeft al een review voor dit nummer.");
            }

            _repository.AddReview(review);
        }

        public List<Review> GetReviews(string trackId)
        {
            return _repository.GetReviews(trackId);
        }

        public void DeleteReview(string trackId, int reviewId)
        {
            _repository.DeleteReview(trackId, reviewId);
        }
    }
}