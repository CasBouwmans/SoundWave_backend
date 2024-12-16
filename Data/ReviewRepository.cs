using Domain;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace App.Data
{
    public class ReviewRepository
    {
        private readonly string _connectionString;

        public ReviewRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddReview(Review review)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"INSERT INTO reviews (TrackId, UserId, ReviewText, Rating) 
                                VALUES (@TrackId, @UserId, @ReviewText, @Rating)";
                command.Parameters.AddWithValue("@TrackId", review.TrackId);
                command.Parameters.AddWithValue("@UserId", review.UserId);
                command.Parameters.AddWithValue("@ReviewText", review.ReviewText);
                command.Parameters.AddWithValue("@Rating", review.Rating);
                command.ExecuteNonQuery();
            }
        }
        public Review GetReviewByUserAndTrack(string userId, string trackId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT TrackId, UserId, ReviewText, Rating 
                                FROM reviews 
                                WHERE UserId = @UserId AND TrackId = @TrackId";
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@TrackId", trackId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Review
                        {
                            TrackId = reader.GetString("TrackId"),
                            UserId = reader.GetString("UserId"),
                            ReviewText = reader.GetString("ReviewText"),
                            Rating = reader.GetInt32("Rating")
                        };
                    }
                }
            }

            return null; // Geen bestaande review gevonden
        }



    }
}
