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

public class DiscountServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IDiscountRepository> _discountRepositoryMock;
    private readonly Mock<IValidator<Discount>> _discountValidatorMock;
    private readonly DiscountService _service;

    public DiscountServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _discountRepositoryMock = new Mock<IDiscountRepository>();
        _discountValidatorMock = new Mock<IValidator<Discount>>();

        _service = new DiscountService(
            _discountRepositoryMock.Object,
            _discountValidatorMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnDiscount_WhenExists()
    {
        var discount = _fixture.Create<Discount>();
        _discountRepositoryMock.Setup(r => r.GetByIdAsync(discount.Id))
            .ReturnsAsync(discount);

        var result = await _service.GetByIdAsync(discount.Id);

        Assert.Equal(discount.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFound_WhenMissing()
    {
        _discountRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Discount?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnDiscount_WhenValid()
    {
        var discount = _fixture.Create<Discount>();

        _discountValidatorMock.Setup(v => v.ValidateAsync(discount, default))
            .ReturnsAsync(new ValidationResult());

        _discountRepositoryMock.Setup(r => r.AddAsync(discount))
            .ReturnsAsync(discount);

        _discountRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _service.AddAsync(discount);

        Assert.Equal(discount.Id, result.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var discount = _fixture.Create<Discount>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Field", "Invalid")
        });

        _discountValidatorMock.Setup(v => v.ValidateAsync(discount, default))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _service.AddAsync(discount));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowCreationException_WhenRepositoryFails()
    {
        var discount = _fixture.Create<Discount>();

        _discountValidatorMock.Setup(v => v.ValidateAsync(discount, default))
            .ReturnsAsync(new ValidationResult());

        _discountRepositoryMock.Setup(r => r.AddAsync(discount))
            .ReturnsAsync((Discount?)null!);

        _discountRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<CreationException>(() => _service.AddAsync(discount));
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenValid()
    {
        var discount = _fixture.Create<Discount>();

        _discountValidatorMock
            .Setup(v => v.ValidateAsync(discount, default))
            .ReturnsAsync(new ValidationResult());

        _discountRepositoryMock
            .Setup(r => r.UpdateAsync(discount))
            .ReturnsAsync(true);

        _discountRepositoryMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await _service.UpdateAsync(discount);

        _discountValidatorMock.Verify(v => v.ValidateAsync(discount, default), Times.Once);
        _discountRepositoryMock.Verify(r => r.UpdateAsync(discount), Times.Once);
        _discountRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var discount = _fixture.Create<Discount>();

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required")
        });

        _discountValidatorMock.Setup(v => v.ValidateAsync(discount, default))
            .ReturnsAsync(validationResult);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(discount));

        Assert.Contains("Name is required", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenRepoReturnsFalse()
    {
        var discount = _fixture.Create<Discount>();

        _discountValidatorMock.Setup(v => v.ValidateAsync(discount, default))
            .ReturnsAsync(new ValidationResult());

        _discountRepositoryMock.Setup(r => r.UpdateAsync(discount))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(discount));
    }

    [Fact]
    public async Task DeleteAsync_ShouldSucceed_WhenExists()
    {
        var id = _fixture.Create<int>();

        _discountRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
        _discountRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id);

        _discountRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _discountRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFound_WhenMissing()
    {
        var id = _fixture.Create<int>();

        _discountRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id));

        _discountRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _discountRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
