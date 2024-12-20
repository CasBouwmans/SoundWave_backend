using System;
using Moq;
using Xunit;

public class ReviewRepositoryTests
{
    private readonly Mock<IReviewRepository> _reviewRepositoryMock;

    public ReviewRepositoryTests()
    {
        _reviewRepositoryMock = new Mock<IReviewRepository>();
    }

    // Tests for ReviewRepository methods
    [Fact]
    public void DeleteReview_ShouldThrowException_WhenReviewDoesNotExist()
    {
        _reviewRepositoryMock.Setup(r => r.ReviewExists(1, 1)).Returns(false);
        Assert.Throws<KeyNotFoundException>(() => _reviewRepositoryMock.Object.DeleteReview(1, 1));
    }

    [Fact]
    public void DeleteReview_ShouldRemoveReview_WhenReviewExists()
    {
        var review = new Review { UserId = 1, TrackId = 1 };
        _reviewRepositoryMock.Setup(r => r.ReviewExists(1, 1)).Returns(true);
        _reviewRepositoryMock.Setup(r => r.DeleteReview(1, 1));
        _reviewRepositoryMock.Object.DeleteReview(1, 1);
        _reviewRepositoryMock.Verify(r => r.DeleteReview(1, 1), Times.Once);
    }
}
