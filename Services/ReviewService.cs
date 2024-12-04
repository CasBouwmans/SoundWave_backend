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
            // Eventuele logica of validatie toevoegen
            //if (review.Rating < 1 || review.Rating > 5)
            //{
            //    throw new ArgumentException("Rating moet tussen 1 en 5 liggen.");
            //}

            _repository.AddReview(review);
        }
        public List<Review> GetReviews()
        {
            return _context.Reviews.ToList(); // Haal alle reviews op
        }
    }
}
