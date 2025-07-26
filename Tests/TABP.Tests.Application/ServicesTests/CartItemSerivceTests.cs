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

public class CartItemServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<ICartItemRepository> _cartItemRepositoryMock;
    private readonly Mock<IValidator<CartItem>> _cartItemValidatorMock;
    private readonly CartItemService _service;

    public CartItemServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _cartItemRepositoryMock = new Mock<ICartItemRepository>();
        _cartItemValidatorMock = new Mock<IValidator<CartItem>>();

        _service = new CartItemService(
            _cartItemRepositoryMock.Object,
            _cartItemValidatorMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCartItem_WhenExists()
    {
        var cartItem = _fixture.Create<CartItem>();
        _cartItemRepositoryMock.Setup(r => r.GetByIdAsync(cartItem.Id))
            .ReturnsAsync(cartItem);

        var result = await _service.GetByIdAsync(cartItem.Id);

        Assert.Equal(cartItem.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFound_WhenMissing()
    {
        _cartItemRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((CartItem?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnCartItem_WhenValid()
    {
        var cartItem = _fixture.Create<CartItem>();

        _cartItemValidatorMock.Setup(v => v.ValidateAsync(cartItem, default))
            .ReturnsAsync(new ValidationResult());

        _cartItemRepositoryMock.Setup(r => r.AddAsync(cartItem))
            .ReturnsAsync(cartItem);

        _cartItemRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _service.AddAsync(cartItem);

        Assert.Equal(cartItem.Id, result.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var cartItem = _fixture.Create<CartItem>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Field", "Invalid")
        });

        _cartItemValidatorMock.Setup(v => v.ValidateAsync(cartItem, default))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _service.AddAsync(cartItem));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowCreationException_WhenRepositoryFails()
    {
        var cartItem = _fixture.Create<CartItem>();

        _cartItemValidatorMock.Setup(v => v.ValidateAsync(cartItem, default))
            .ReturnsAsync(new ValidationResult());

        _cartItemRepositoryMock.Setup(r => r.AddAsync(cartItem))
            .ReturnsAsync((CartItem?)null!);

        _cartItemRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<CreationException>(() => _service.AddAsync(cartItem));
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenValid()
    {
        var cartItem = _fixture.Create<CartItem>();

        _cartItemValidatorMock
            .Setup(v => v.ValidateAsync(cartItem, default))
            .ReturnsAsync(new ValidationResult());

        _cartItemRepositoryMock
            .Setup(r => r.UpdateAsync(cartItem))
            .ReturnsAsync(true);

        _cartItemRepositoryMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await _service.UpdateAsync(cartItem);

        _cartItemValidatorMock.Verify(v => v.ValidateAsync(cartItem, default), Times.Once);
        _cartItemRepositoryMock.Verify(r => r.UpdateAsync(cartItem), Times.Once);
        _cartItemRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var cartItem = _fixture.Create<CartItem>();

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Quantity", "Quantity must be positive")
        });

        _cartItemValidatorMock.Setup(v => v.ValidateAsync(cartItem, default))
            .ReturnsAsync(validationResult);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(cartItem));

        Assert.Contains("Quantity must be positive", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenRepoReturnsFalse()
    {
        var cartItem = _fixture.Create<CartItem>();

        _cartItemValidatorMock.Setup(v => v.ValidateAsync(cartItem, default))
            .ReturnsAsync(new ValidationResult());

        _cartItemRepositoryMock.Setup(r => r.UpdateAsync(cartItem))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(cartItem));
    }

    [Fact]
    public async Task DeleteAsync_ShouldSucceed_WhenExists()
    {
        var id = _fixture.Create<int>();

        _cartItemRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
        _cartItemRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id);

        _cartItemRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _cartItemRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFound_WhenMissing()
    {
        var id = _fixture.Create<int>();

        _cartItemRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id));

        _cartItemRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _cartItemRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
