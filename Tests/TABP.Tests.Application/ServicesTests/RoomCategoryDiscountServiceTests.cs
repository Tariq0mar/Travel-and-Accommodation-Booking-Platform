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

public class RoomCategoryDiscountServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IRoomCategoryDiscountRepository> _roomCategoryDiscountRepositoryMock;
    private readonly Mock<IValidator<RoomCategoryDiscount>> _roomCategoryDiscountValidatorMock;
    private readonly RoomCategoryDiscountService _service;

    public RoomCategoryDiscountServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _roomCategoryDiscountRepositoryMock = new Mock<IRoomCategoryDiscountRepository>();
        _roomCategoryDiscountValidatorMock = new Mock<IValidator<RoomCategoryDiscount>>();

        _service = new RoomCategoryDiscountService(
            _roomCategoryDiscountRepositoryMock.Object,
            _roomCategoryDiscountValidatorMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnRoomCategoryDiscount_WhenExists()
    {
        var discount = _fixture.Create<RoomCategoryDiscount>();
        _roomCategoryDiscountRepositoryMock.Setup(r => r.GetByIdAsync(discount.Id))
            .ReturnsAsync(discount);

        var result = await _service.GetByIdAsync(discount.Id);

        Assert.Equal(discount.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFound_WhenMissing()
    {
        _roomCategoryDiscountRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((RoomCategoryDiscount?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnRoomCategoryDiscount_WhenValid()
    {
        var discount = _fixture.Create<RoomCategoryDiscount>();

        _roomCategoryDiscountValidatorMock.Setup(v => v.ValidateAsync(discount, default))
            .ReturnsAsync(new ValidationResult());

        _roomCategoryDiscountRepositoryMock.Setup(r => r.AddAsync(discount))
            .ReturnsAsync(discount);

        _roomCategoryDiscountRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _service.AddAsync(discount);

        Assert.Equal(discount.Id, result.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var discount = _fixture.Create<RoomCategoryDiscount>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Field", "Invalid")
        });

        _roomCategoryDiscountValidatorMock.Setup(v => v.ValidateAsync(discount, default))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _service.AddAsync(discount));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowCreationException_WhenRepositoryFails()
    {
        var discount = _fixture.Create<RoomCategoryDiscount>();

        _roomCategoryDiscountValidatorMock.Setup(v => v.ValidateAsync(discount, default))
            .ReturnsAsync(new ValidationResult());

        _roomCategoryDiscountRepositoryMock.Setup(r => r.AddAsync(discount))
            .ReturnsAsync((RoomCategoryDiscount?)null!);

        _roomCategoryDiscountRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<CreationException>(() => _service.AddAsync(discount));
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenValid()
    {
        var discount = _fixture.Create<RoomCategoryDiscount>();

        _roomCategoryDiscountValidatorMock
            .Setup(v => v.ValidateAsync(discount, default))
            .ReturnsAsync(new ValidationResult());

        _roomCategoryDiscountRepositoryMock
            .Setup(r => r.UpdateAsync(discount))
            .ReturnsAsync(true);

        _roomCategoryDiscountRepositoryMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await _service.UpdateAsync(discount);

        _roomCategoryDiscountValidatorMock.Verify(v => v.ValidateAsync(discount, default), Times.Once);
        _roomCategoryDiscountRepositoryMock.Verify(r => r.UpdateAsync(discount), Times.Once);
        _roomCategoryDiscountRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var discount = _fixture.Create<RoomCategoryDiscount>();

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("DiscountPercent", "DiscountPercent is required")
        });

        _roomCategoryDiscountValidatorMock.Setup(v => v.ValidateAsync(discount, default))
            .ReturnsAsync(validationResult);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(discount));

        Assert.Contains("DiscountPercent is required", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenRepoReturnsFalse()
    {
        var discount = _fixture.Create<RoomCategoryDiscount>();

        _roomCategoryDiscountValidatorMock.Setup(v => v.ValidateAsync(discount, default))
            .ReturnsAsync(new ValidationResult());

        _roomCategoryDiscountRepositoryMock.Setup(r => r.UpdateAsync(discount))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(discount));
    }

    [Fact]
    public async Task DeleteAsync_ShouldSucceed_WhenExists()
    {
        var id = _fixture.Create<int>();

        _roomCategoryDiscountRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
        _roomCategoryDiscountRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id);

        _roomCategoryDiscountRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _roomCategoryDiscountRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFound_WhenMissing()
    {
        var id = _fixture.Create<int>();

        _roomCategoryDiscountRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id));

        _roomCategoryDiscountRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _roomCategoryDiscountRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
