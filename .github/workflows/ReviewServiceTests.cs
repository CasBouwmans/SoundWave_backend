using System;
using System.Collections.Generic;
using Moq;
using Xunit;

public class ReviewServiceTests
{
    private readonly Mock<IReviewRepository> _reviewRepositoryMock;
    private readonly ReviewService _reviewService;

    public ReviewServiceTests()
    {
        _reviewRepositoryMock = new Mock<IReviewRepository>();
        _reviewService = new ReviewService(_reviewRepositoryMock.Object);
    }

    // Unit Tests for AddReview
    [Fact]
    public void AddReview_ShouldThrowArgumentException_WhenRatingIsOutOfRange()
    {
        var review = new Review { Rating = 6 };
        Assert.Throws<ArgumentException>(() => _reviewService.AddReview(review));
    }

    [Fact]
    public void AddReview_ShouldThrowInvalidOperationException_WhenReviewAlreadyExists()
    {
        var review = new Review { UserId = 1, TrackId = 1 };
        _reviewRepositoryMock.Setup(r => r.ReviewExists(review.UserId, review.TrackId)).Returns(true);
        Assert.Throws<InvalidOperationException>(() => _reviewService.AddReview(review));
    }

    [Fact]
    public void AddReview_ShouldCallAddReview_WhenReviewIsValid()
    {
        var review = new Review { Rating = 4, UserId = 1, TrackId = 1 };
        _reviewService.AddReview(review);
        _reviewRepositoryMock.Verify(r => r.AddReview(review), Times.Once);
    }

    // Unit Tests for GetReviews
    [Fact]
    public void GetReviews_ShouldReturnReviews_WhenTrackIdIsValid()
    {
        var reviews = new List<Review> { new Review { TrackId = 1 } };
        _reviewRepositoryMock.Setup(r => r.GetReviews(1)).Returns(reviews);
        var result = _reviewService.GetReviews(1);
        Assert.Equal(reviews, result);
    }

    [Fact]
    public void GetReviews_ShouldReturnEmptyList_WhenNoReviewsExist()
    {
        _reviewRepositoryMock.Setup(r => r.GetReviews(1)).Returns(new List<Review>());
        var result = _reviewService.GetReviews(1);
        Assert.Empty(result);
    }

    // Unit Tests for DeleteReview
    [Fact]
    public void DeleteReview_ShouldThrowKeyNotFoundException_WhenReviewDoesNotExist()
    {
        _reviewRepositoryMock.Setup(r => r.ReviewExists(1, 1)).Returns(false);
        Assert.Throws<KeyNotFoundException>(() => _reviewService.DeleteReview(1, 1));
    }

    [Fact]
    public void DeleteReview_ShouldCallDeleteReview_WhenReviewExists()
    {
        _reviewRepositoryMock.Setup(r => r.ReviewExists(1, 1)).Returns(true);
        _reviewService.DeleteReview(1, 1);
        _reviewRepositoryMock.Verify(r => r.DeleteReview(1, 1), Times.Once);
    }
}
