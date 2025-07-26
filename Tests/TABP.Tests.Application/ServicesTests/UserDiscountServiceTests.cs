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

public class UserDiscountServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IUserDiscountRepository> _userDiscountRepositoryMock;
    private readonly Mock<IValidator<UserDiscount>> _userDiscountValidatorMock;
    private readonly UserDiscountService _service;

    public UserDiscountServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _userDiscountRepositoryMock = new Mock<IUserDiscountRepository>();
        _userDiscountValidatorMock = new Mock<IValidator<UserDiscount>>();

        _service = new UserDiscountService(
            _userDiscountRepositoryMock.Object,
            _userDiscountValidatorMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnUserDiscount_WhenExists()
    {
        var discount = _fixture.Create<UserDiscount>();
        _userDiscountRepositoryMock.Setup(r => r.GetByIdAsync(discount.Id))
            .ReturnsAsync(discount);

        var result = await _service.GetByIdAsync(discount.Id);

        Assert.Equal(discount.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFound_WhenMissing()
    {
        _userDiscountRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((UserDiscount?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnUserDiscount_WhenValid()
    {
        var discount = _fixture.Create<UserDiscount>();

        _userDiscountValidatorMock.Setup(v => v.ValidateAsync(discount, default))
            .ReturnsAsync(new ValidationResult());

        _userDiscountRepositoryMock.Setup(r => r.AddAsync(discount))
            .ReturnsAsync(discount);

        _userDiscountRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _service.AddAsync(discount);

        Assert.Equal(discount.Id, result.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var discount = _fixture.Create<UserDiscount>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Field", "Invalid")
        });

        _userDiscountValidatorMock.Setup(v => v.ValidateAsync(discount, default))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _service.AddAsync(discount));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowCreationException_WhenRepositoryFails()
    {
        var discount = _fixture.Create<UserDiscount>();

        _userDiscountValidatorMock.Setup(v => v.ValidateAsync(discount, default))
            .ReturnsAsync(new ValidationResult());

        _userDiscountRepositoryMock.Setup(r => r.AddAsync(discount))
            .ReturnsAsync((UserDiscount?)null!);

        _userDiscountRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<CreationException>(() => _service.AddAsync(discount));
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenValid()
    {
        var discount = _fixture.Create<UserDiscount>();

        _userDiscountValidatorMock
            .Setup(v => v.ValidateAsync(discount, default))
            .ReturnsAsync(new ValidationResult());

        _userDiscountRepositoryMock
            .Setup(r => r.UpdateAsync(discount))
            .ReturnsAsync(true);

        _userDiscountRepositoryMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await _service.UpdateAsync(discount);

        _userDiscountValidatorMock.Verify(v => v.ValidateAsync(discount, default), Times.Once);
        _userDiscountRepositoryMock.Verify(r => r.UpdateAsync(discount), Times.Once);
        _userDiscountRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var discount = _fixture.Create<UserDiscount>();

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("DiscountPercent", "DiscountPercent is required")
        });

        _userDiscountValidatorMock.Setup(v => v.ValidateAsync(discount, default))
            .ReturnsAsync(validationResult);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(discount));

        Assert.Contains("DiscountPercent is required", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenRepoReturnsFalse()
    {
        var discount = _fixture.Create<UserDiscount>();

        _userDiscountValidatorMock.Setup(v => v.ValidateAsync(discount, default))
            .ReturnsAsync(new ValidationResult());

        _userDiscountRepositoryMock.Setup(r => r.UpdateAsync(discount))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(discount));
    }

    [Fact]
    public async Task DeleteAsync_ShouldSucceed_WhenExists()
    {
        var id = _fixture.Create<int>();

        _userDiscountRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
        _userDiscountRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id);

        _userDiscountRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _userDiscountRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFound_WhenMissing()
    {
        var id = _fixture.Create<int>();

        _userDiscountRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id));

        _userDiscountRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _userDiscountRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
