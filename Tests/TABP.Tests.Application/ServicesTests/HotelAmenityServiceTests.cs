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

public class HotelAmenityServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IHotelAmenityRepository> _hotelAmenityRepositoryMock;
    private readonly Mock<IValidator<HotelAmenity>> _hotelAmenityValidatorMock;
    private readonly HotelAmenityService _service;

    public HotelAmenityServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _hotelAmenityRepositoryMock = new Mock<IHotelAmenityRepository>();
        _hotelAmenityValidatorMock = new Mock<IValidator<HotelAmenity>>();

        _service = new HotelAmenityService(
            _hotelAmenityRepositoryMock.Object,
            _hotelAmenityValidatorMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnHotelAmenity_WhenExists()
    {
        var hotelAmenity = _fixture.Create<HotelAmenity>();
        _hotelAmenityRepositoryMock.Setup(r => r.GetByIdAsync(hotelAmenity.Id))
            .ReturnsAsync(hotelAmenity);

        var result = await _service.GetByIdAsync(hotelAmenity.Id);

        Assert.Equal(hotelAmenity.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFound_WhenMissing()
    {
        _hotelAmenityRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((HotelAmenity?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnHotelAmenity_WhenValid()
    {
        var hotelAmenity = _fixture.Create<HotelAmenity>();

        _hotelAmenityValidatorMock.Setup(v => v.ValidateAsync(hotelAmenity, default))
            .ReturnsAsync(new ValidationResult());

        _hotelAmenityRepositoryMock.Setup(r => r.AddAsync(hotelAmenity))
            .ReturnsAsync(hotelAmenity);

        _hotelAmenityRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _service.AddAsync(hotelAmenity);

        Assert.Equal(hotelAmenity.Id, result.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var hotelAmenity = _fixture.Create<HotelAmenity>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Field", "Invalid")
        });

        _hotelAmenityValidatorMock.Setup(v => v.ValidateAsync(hotelAmenity, default))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _service.AddAsync(hotelAmenity));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowCreationException_WhenRepositoryFails()
    {
        var hotelAmenity = _fixture.Create<HotelAmenity>();

        _hotelAmenityValidatorMock.Setup(v => v.ValidateAsync(hotelAmenity, default))
            .ReturnsAsync(new ValidationResult());

        _hotelAmenityRepositoryMock.Setup(r => r.AddAsync(hotelAmenity))
            .ReturnsAsync((HotelAmenity?)null!);

        _hotelAmenityRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<CreationException>(() => _service.AddAsync(hotelAmenity));
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenValid()
    {
        var hotelAmenity = _fixture.Create<HotelAmenity>();

        _hotelAmenityValidatorMock
            .Setup(v => v.ValidateAsync(hotelAmenity, default))
            .ReturnsAsync(new ValidationResult());

        _hotelAmenityRepositoryMock
            .Setup(r => r.UpdateAsync(hotelAmenity))
            .ReturnsAsync(true);

        _hotelAmenityRepositoryMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await _service.UpdateAsync(hotelAmenity);

        _hotelAmenityValidatorMock.Verify(v => v.ValidateAsync(hotelAmenity, default), Times.Once);
        _hotelAmenityRepositoryMock.Verify(r => r.UpdateAsync(hotelAmenity), Times.Once);
        _hotelAmenityRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var hotelAmenity = _fixture.Create<HotelAmenity>();

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required")
        });

        _hotelAmenityValidatorMock.Setup(v => v.ValidateAsync(hotelAmenity, default))
            .ReturnsAsync(validationResult);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(hotelAmenity));

        Assert.Contains("Name is required", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenRepoReturnsFalse()
    {
        var hotelAmenity = _fixture.Create<HotelAmenity>();

        _hotelAmenityValidatorMock.Setup(v => v.ValidateAsync(hotelAmenity, default))
            .ReturnsAsync(new ValidationResult());

        _hotelAmenityRepositoryMock.Setup(r => r.UpdateAsync(hotelAmenity))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(hotelAmenity));
    }

    [Fact]
    public async Task DeleteAsync_ShouldSucceed_WhenExists()
    {
        var id = _fixture.Create<int>();

        _hotelAmenityRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
        _hotelAmenityRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id);

        _hotelAmenityRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _hotelAmenityRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFound_WhenMissing()
    {
        var id = _fixture.Create<int>();

        _hotelAmenityRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id));

        _hotelAmenityRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _hotelAmenityRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
