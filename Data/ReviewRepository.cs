using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class ReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public Review GetReviewByUserAndTrack(string userId, string trackId)
        {
            return _context.Reviews
                .FirstOrDefault(r => r.UserId == userId && r.TrackId == trackId);
        }

        public void DeleteReview(string trackId, int reviewId)
        {
            var review = _context.Reviews
                .FirstOrDefault(r => r.TrackId == trackId && r.Id == reviewId);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
            }
        }

        public List<Review> GetReviews(string trackId)
        {
            return _context.Reviews.Where(r => r.TrackId == trackId).ToList();
        }
    }
}