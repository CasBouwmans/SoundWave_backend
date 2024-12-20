using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class ReviewControllerTests
{
    private readonly Mock<IReviewRepository> _reviewRepositoryMock;
    private readonly ReviewService _reviewService;
    private readonly ReviewController _reviewController;

    public ReviewControllerTests()
    {
        _reviewRepositoryMock = new Mock<IReviewRepository>();
        _reviewService = new ReviewService(_reviewRepositoryMock.Object);
        _reviewController = new ReviewController(_reviewService);
    }

    // Unit Tests for AddReview
    [Fact]
    public void AddReview_ShouldReturnOk_WhenReviewIsValid()
    {
        var review = new Review { Rating = 4, UserId = 1, TrackId = 1 };
        var result = _reviewController.AddReview(review);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void AddReview_ShouldReturnBadRequest_WhenRatingIsOutOfRange()
    {
        var review = new Review { Rating = 6 };
        var result = _reviewController.AddReview(review);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void AddReview_ShouldReturnConflict_WhenReviewAlreadyExists()
    {
        var review = new Review { UserId = 1, TrackId = 1 };
        _reviewRepositoryMock.Setup(r => r.ReviewExists(review.UserId, review.TrackId)).Returns(true);
        var result = _reviewController.AddReview(review);
        Assert.IsType<ConflictObjectResult>(result);
    }

    // Unit Tests for GetReviews
    [Fact]
    public void GetReviews_ShouldReturnOk_WhenReviewsExist()
    {
        var reviews = new List<Review> { new Review { TrackId = 1 } };
        _reviewRepositoryMock.Setup(r => r.GetReviews(1)).Returns(reviews);
        var result = _reviewController.GetReviews(1);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetReviews_ShouldReturnNotFound_WhenNoReviewsExist()
    {
        _reviewRepositoryMock.Setup(r => r.GetReviews(1)).Returns(new List<Review>());
        var result = _reviewController.GetReviews(1);
        Assert.IsType<NotFoundObjectResult>(result);
    }

    // Unit Tests for DeleteReview
    [Fact]
    public void DeleteReview_ShouldReturnOk_WhenReviewExists()
    {
        _reviewRepositoryMock.Setup(r => r.ReviewExists(1, 1)).Returns(true);
        var result = _reviewController.DeleteReview(1, 1);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void DeleteReview_ShouldReturnNotFound_WhenReviewDoesNotExist()
    {
        _reviewRepositoryMock.Setup(r => r.ReviewExists(1, 1)).Returns(false);
        var result = _reviewController.DeleteReview(1, 1);
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
