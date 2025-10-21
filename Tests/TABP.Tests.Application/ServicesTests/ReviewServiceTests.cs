using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using TABP.Application.Services;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories;
using Xunit;

public class ReviewServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IReviewRepository> _reviewRepositoryMock;
    private readonly Mock<IValidator<Review>> _reviewValidatorMock;
    private readonly ReviewService _service;

    public ReviewServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _reviewRepositoryMock = new Mock<IReviewRepository>();
        _reviewValidatorMock = new Mock<IValidator<Review>>();

        _service = new ReviewService(
            _reviewRepositoryMock.Object,
            _reviewValidatorMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnReview_WhenExists()
    {
        var review = _fixture.Create<Review>();
        _reviewRepositoryMock.Setup(r => r.GetByIdAsync(review.Id))
            .ReturnsAsync(review);

        var result = await _service.GetByIdAsync(review.Id);

        Assert.Equal(review.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFound_WhenMissing()
    {
        _reviewRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Review?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnReview_WhenValid()
    {
        var review = _fixture.Create<Review>();

        _reviewValidatorMock.Setup(v => v.ValidateAsync(review, default))
            .ReturnsAsync(new ValidationResult());

        _reviewRepositoryMock.Setup(r => r.AddAsync(review))
            .ReturnsAsync(review);

        _reviewRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _service.AddAsync(review);

        Assert.Equal(review.Id, result.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var review = _fixture.Create<Review>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Field", "Invalid")
        });

        _reviewValidatorMock.Setup(v => v.ValidateAsync(review, default))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _service.AddAsync(review));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowCreationException_WhenRepositoryFails()
    {
        var review = _fixture.Create<Review>();

        _reviewValidatorMock.Setup(v => v.ValidateAsync(review, default))
            .ReturnsAsync(new ValidationResult());

        _reviewRepositoryMock.Setup(r => r.AddAsync(review))
            .ReturnsAsync((Review?)null!);

        _reviewRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<CreationException>(() => _service.AddAsync(review));
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenValid()
    {
        var review = _fixture.Create<Review>();

        _reviewValidatorMock
            .Setup(v => v.ValidateAsync(review, default))
            .ReturnsAsync(new ValidationResult());

        _reviewRepositoryMock
            .Setup(r => r.UpdateAsync(review))
            .ReturnsAsync(true);

        _reviewRepositoryMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await _service.UpdateAsync(review);

        _reviewValidatorMock.Verify(v => v.ValidateAsync(review, default), Times.Once);
        _reviewRepositoryMock.Verify(r => r.UpdateAsync(review), Times.Once);
        _reviewRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var review = _fixture.Create<Review>();

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Comment", "Comment is required")
        });

        _reviewValidatorMock.Setup(v => v.ValidateAsync(review, default))
            .ReturnsAsync(validationResult);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(review));

        Assert.Contains("Comment is required", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenRepoReturnsFalse()
    {
        var review = _fixture.Create<Review>();

        _reviewValidatorMock.Setup(v => v.ValidateAsync(review, default))
            .ReturnsAsync(new ValidationResult());

        _reviewRepositoryMock.Setup(r => r.UpdateAsync(review))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(review));
    }

    [Fact]
    public async Task DeleteAsync_ShouldSucceed_WhenExists()
    {
        var id = _fixture.Create<int>();

        _reviewRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
        _reviewRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id);

        _reviewRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _reviewRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFound_WhenMissing()
    {
        var id = _fixture.Create<int>();

        _reviewRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id));

        _reviewRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _reviewRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
