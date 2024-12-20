using System;
using Moq;
using Xunit;

public class IntegrationTests
{
    private readonly Mock<IReviewRepository> _reviewRepositoryMock;

    public IntegrationTests()
    {
        _reviewRepositoryMock = new Mock<IReviewRepository>();
    }

    // Integration Tests for Database Interactions
    [Fact]
    public void Database_ShouldAddReview()
    {
        var review = new Review { Rating = 4, UserId = 1, TrackId = 1 };
        _reviewRepositoryMock.Object.AddReview(review);
        _reviewRepositoryMock.Verify(r => r.AddReview(review), Times.Once);
    }

    [Fact]
    public void Database_ShouldGetReview()
    {
        var review = new Review { Rating = 4, UserId = 1, TrackId = 1 };
        _reviewRepositoryMock.Setup(r => r.GetReview(1, 1)).Returns(review);
        var result = _reviewRepositoryMock.Object.GetReview(1, 1);
        Assert.Equal(review, result);
    }

    [Fact]
    public void Database_ShouldDeleteReview()
    {
        var review = new Review { Rating = 4, UserId = 1, TrackId = 1 };
        _reviewRepositoryMock.Setup(r => r.ReviewExists(1, 1)).Returns(true);
        _reviewRepositoryMock.Object.DeleteReview(1, 1);
        _reviewRepositoryMock.Verify(r => r.DeleteReview(1, 1), Times.Once);
    }

    [Fact]
    public void Database_ShouldThrowException_WhenDeletingNonExistentReview()
    {
        _reviewRepositoryMock.Setup(r => r.ReviewExists(1, 1)).Returns(false);
        Assert.Throws<KeyNotFoundException>(() => _reviewRepositoryMock.Object.DeleteReview(1, 1));
    }
}
