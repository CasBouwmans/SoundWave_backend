using Domain;
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
                command.CommandText = @"INSERT INTO reviews (TrackId, UserId, ReviewText) 
                                VALUES (@TrackId, @UserId, @ReviewText)";
                command.Parameters.AddWithValue("@TrackId", review.TrackId);
                command.Parameters.AddWithValue("@UserId", review.UserId);
                command.Parameters.AddWithValue("@ReviewText", review.ReviewText);
                //command.Parameters.AddWithValue("@Rating", review.Rating); // Voeg Rating toe
                command.ExecuteNonQuery();
            }
        }

    }
}
