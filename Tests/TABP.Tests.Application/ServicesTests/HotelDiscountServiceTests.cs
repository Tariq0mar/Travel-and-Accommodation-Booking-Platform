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

public class HotelDiscountServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IHotelDiscountRepository> _hotelDiscountRepositoryMock;
    private readonly Mock<IValidator<HotelDiscount>> _hotelDiscountValidatorMock;
    private readonly HotelDiscountService _service;

    public HotelDiscountServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _hotelDiscountRepositoryMock = new Mock<IHotelDiscountRepository>();
        _hotelDiscountValidatorMock = new Mock<IValidator<HotelDiscount>>();

        _service = new HotelDiscountService(
            _hotelDiscountRepositoryMock.Object,
            _hotelDiscountValidatorMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnHotelDiscount_WhenExists()
    {
        var hotelDiscount = _fixture.Create<HotelDiscount>();
        _hotelDiscountRepositoryMock.Setup(r => r.GetByIdAsync(hotelDiscount.Id))
            .ReturnsAsync(hotelDiscount);

        var result = await _service.GetByIdAsync(hotelDiscount.Id);

        Assert.Equal(hotelDiscount.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFound_WhenMissing()
    {
        _hotelDiscountRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((HotelDiscount?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnHotelDiscount_WhenValid()
    {
        var hotelDiscount = _fixture.Create<HotelDiscount>();

        _hotelDiscountValidatorMock.Setup(v => v.ValidateAsync(hotelDiscount, default))
            .ReturnsAsync(new ValidationResult());

        _hotelDiscountRepositoryMock.Setup(r => r.AddAsync(hotelDiscount))
            .ReturnsAsync(hotelDiscount);

        _hotelDiscountRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _service.AddAsync(hotelDiscount);

        Assert.Equal(hotelDiscount.Id, result.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var hotelDiscount = _fixture.Create<HotelDiscount>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Field", "Invalid")
        });

        _hotelDiscountValidatorMock.Setup(v => v.ValidateAsync(hotelDiscount, default))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _service.AddAsync(hotelDiscount));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowCreationException_WhenRepositoryFails()
    {
        var hotelDiscount = _fixture.Create<HotelDiscount>();

        _hotelDiscountValidatorMock.Setup(v => v.ValidateAsync(hotelDiscount, default))
            .ReturnsAsync(new ValidationResult());

        _hotelDiscountRepositoryMock.Setup(r => r.AddAsync(hotelDiscount))
            .ReturnsAsync((HotelDiscount?)null!);

        _hotelDiscountRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<CreationException>(() => _service.AddAsync(hotelDiscount));
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenValid()
    {
        var hotelDiscount = _fixture.Create<HotelDiscount>();

        _hotelDiscountValidatorMock
            .Setup(v => v.ValidateAsync(hotelDiscount, default))
            .ReturnsAsync(new ValidationResult());

        _hotelDiscountRepositoryMock
            .Setup(r => r.UpdateAsync(hotelDiscount))
            .ReturnsAsync(true);

        _hotelDiscountRepositoryMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await _service.UpdateAsync(hotelDiscount);

        _hotelDiscountValidatorMock.Verify(v => v.ValidateAsync(hotelDiscount, default), Times.Once);
        _hotelDiscountRepositoryMock.Verify(r => r.UpdateAsync(hotelDiscount), Times.Once);
        _hotelDiscountRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var hotelDiscount = _fixture.Create<HotelDiscount>();

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required")
        });

        _hotelDiscountValidatorMock.Setup(v => v.ValidateAsync(hotelDiscount, default))
            .ReturnsAsync(validationResult);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(hotelDiscount));

        Assert.Contains("Name is required", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenRepoReturnsFalse()
    {
        var hotelDiscount = _fixture.Create<HotelDiscount>();

        _hotelDiscountValidatorMock.Setup(v => v.ValidateAsync(hotelDiscount, default))
            .ReturnsAsync(new ValidationResult());

        _hotelDiscountRepositoryMock.Setup(r => r.UpdateAsync(hotelDiscount))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(hotelDiscount));
    }

    [Fact]
    public async Task DeleteAsync_ShouldSucceed_WhenExists()
    {
        var id = _fixture.Create<int>();

        _hotelDiscountRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
        _hotelDiscountRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id);

        _hotelDiscountRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _hotelDiscountRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFound_WhenMissing()
    {
        var id = _fixture.Create<int>();

        _hotelDiscountRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id));

        _hotelDiscountRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _hotelDiscountRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
