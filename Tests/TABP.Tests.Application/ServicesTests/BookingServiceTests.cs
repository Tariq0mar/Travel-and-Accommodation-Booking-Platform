using AutoFixture;
using AutoFixture.AutoMoq;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using TABP.Application.Services;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories;

public class BookingServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IBookingRepository> _bookingRepositoryMock;
    private readonly Mock<IValidator<Booking>> _bookingValidatorMock;
    private readonly BookingService _service;

    public BookingServiceTests()
    {
        _fixture = new Fixture();

        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _bookingRepositoryMock = new Mock<IBookingRepository>();
        _bookingValidatorMock = new Mock<IValidator<Booking>>();

        _service = new BookingService(
            _bookingRepositoryMock.Object,
            _bookingValidatorMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnBooking_WhenExists()
    {
        var booking = _fixture.Create<Booking>();
        _bookingRepositoryMock.Setup(r => r.GetByIdAsync(booking.Id))
            .ReturnsAsync(booking);

        var result = await _service.GetByIdAsync(booking.Id);

        Assert.Equal(booking.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFound_WhenMissing()
    {
        _bookingRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Booking?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnBooking_WhenValid()
    {
        var booking = _fixture.Create<Booking>();

        _bookingValidatorMock.Setup(v => v.ValidateAsync(booking, default))
            .ReturnsAsync(new ValidationResult());

        _bookingRepositoryMock.Setup(r => r.AddAsync(booking))
            .ReturnsAsync(booking);

        _bookingRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _service.AddAsync(booking);

        Assert.Equal(booking.Id, result.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var booking = _fixture.Create<Booking>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Field", "Invalid")
        });

        _bookingValidatorMock.Setup(v => v.ValidateAsync(booking, default))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _service.AddAsync(booking));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowCreationException_WhenRepositoryFails()
    {
        var booking = _fixture.Create<Booking>();

        _bookingValidatorMock.Setup(v => v.ValidateAsync(booking, default))
            .ReturnsAsync(new ValidationResult());

        _bookingRepositoryMock.Setup(r => r.AddAsync(booking))
            .ReturnsAsync((Booking?)null!);
        
        _bookingRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<CreationException>(() => _service.AddAsync(booking));
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenValid()
    {
        var booking = _fixture.Create<Booking>();

        _bookingValidatorMock
            .Setup(v => v.ValidateAsync(booking, default))
            .ReturnsAsync(new ValidationResult());

        _bookingRepositoryMock
            .Setup(r => r.UpdateAsync(booking))
            .ReturnsAsync(true);

        _bookingRepositoryMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await _service.UpdateAsync(booking);

        _bookingValidatorMock.Verify(v => v.ValidateAsync(booking, default), Times.Once);
        _bookingRepositoryMock.Verify(r => r.UpdateAsync(booking), Times.Once);
        _bookingRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var booking = _fixture.Create<Booking>();

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("UserId", "UserId is required")
        });

        _bookingValidatorMock.Setup(v => v.ValidateAsync(booking, default))
            .ReturnsAsync(validationResult);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(booking));

        Assert.Contains("UserId is required", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenRepoReturnsFalse()
    {
        var booking = _fixture.Create<Booking>();

        _bookingValidatorMock.Setup(v => v.ValidateAsync(booking, default))
            .ReturnsAsync(new ValidationResult());

        _bookingRepositoryMock.Setup(r => r.UpdateAsync(booking))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(booking));
    }

    [Fact]
    public async Task DeleteAsync_ShouldSucceed_WhenExists()
    {
        var id = _fixture.Create<int>();

        _bookingRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
        _bookingRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id);

        _bookingRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _bookingRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFound_WhenMissing()
    {
        var id = _fixture.Create<int>();

        _bookingRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id));

        _bookingRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _bookingRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
