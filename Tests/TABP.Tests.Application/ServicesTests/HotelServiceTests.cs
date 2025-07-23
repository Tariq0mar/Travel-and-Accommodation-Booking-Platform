using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using TABP.Application.Services.CRUD;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories.CRUD;
using Xunit;

public class HotelServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IHotelRepository> _hotelRepositoryMock;
    private readonly Mock<IValidator<Hotel>> _hotelValidatorMock;
    private readonly HotelService _service;

    public HotelServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _hotelRepositoryMock = new Mock<IHotelRepository>();
        _hotelValidatorMock = new Mock<IValidator<Hotel>>();

        _service = new HotelService(
            _hotelRepositoryMock.Object,
            _hotelValidatorMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnHotel_WhenExists()
    {
        var hotel = _fixture.Create<Hotel>();
        _hotelRepositoryMock.Setup(r => r.GetByIdAsync(hotel.Id))
            .ReturnsAsync(hotel);

        var result = await _service.GetByIdAsync(hotel.Id);

        Assert.Equal(hotel.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFound_WhenMissing()
    {
        _hotelRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Hotel?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnHotel_WhenValid()
    {
        var hotel = _fixture.Create<Hotel>();

        _hotelValidatorMock.Setup(v => v.ValidateAsync(hotel, default))
            .ReturnsAsync(new ValidationResult());

        _hotelRepositoryMock.Setup(r => r.AddAsync(hotel))
            .ReturnsAsync(hotel);

        _hotelRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _service.AddAsync(hotel);

        Assert.Equal(hotel.Id, result.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var hotel = _fixture.Create<Hotel>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Field", "Invalid")
        });

        _hotelValidatorMock.Setup(v => v.ValidateAsync(hotel, default))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _service.AddAsync(hotel));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowCreationException_WhenRepositoryFails()
    {
        var hotel = _fixture.Create<Hotel>();

        _hotelValidatorMock.Setup(v => v.ValidateAsync(hotel, default))
            .ReturnsAsync(new ValidationResult());

        _hotelRepositoryMock.Setup(r => r.AddAsync(hotel))
            .ReturnsAsync((Hotel?)null!);

        _hotelRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<CreationException>(() => _service.AddAsync(hotel));
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenValid()
    {
        var hotel = _fixture.Create<Hotel>();

        _hotelValidatorMock
            .Setup(v => v.ValidateAsync(hotel, default))
            .ReturnsAsync(new ValidationResult());

        _hotelRepositoryMock
            .Setup(r => r.UpdateAsync(hotel))
            .ReturnsAsync(true);

        _hotelRepositoryMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await _service.UpdateAsync(hotel);

        _hotelValidatorMock.Verify(v => v.ValidateAsync(hotel, default), Times.Once);
        _hotelRepositoryMock.Verify(r => r.UpdateAsync(hotel), Times.Once);
        _hotelRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var hotel = _fixture.Create<Hotel>();

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required")
        });

        _hotelValidatorMock.Setup(v => v.ValidateAsync(hotel, default))
            .ReturnsAsync(validationResult);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(hotel));

        Assert.Contains("Name is required", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenRepoReturnsFalse()
    {
        var hotel = _fixture.Create<Hotel>();

        _hotelValidatorMock.Setup(v => v.ValidateAsync(hotel, default))
            .ReturnsAsync(new ValidationResult());

        _hotelRepositoryMock.Setup(r => r.UpdateAsync(hotel))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(hotel));
    }

    [Fact]
    public async Task DeleteAsync_ShouldSucceed_WhenExists()
    {
        var id = _fixture.Create<int>();

        _hotelRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
        _hotelRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id);

        _hotelRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _hotelRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFound_WhenMissing()
    {
        var id = _fixture.Create<int>();

        _hotelRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id));

        _hotelRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _hotelRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
