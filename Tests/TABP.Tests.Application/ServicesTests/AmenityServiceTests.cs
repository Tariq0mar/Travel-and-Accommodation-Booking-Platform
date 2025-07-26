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

public class AmenityServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IAmenityRepository> _amenityRepositoryMock;
    private readonly Mock<IValidator<Amenity>> _amenityValidatorMock;
    private readonly AmenityService _service;

    public AmenityServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _amenityRepositoryMock = new Mock<IAmenityRepository>();
        _amenityValidatorMock = new Mock<IValidator<Amenity>>();

        _service = new AmenityService(
            _amenityRepositoryMock.Object,
            _amenityValidatorMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnAmenity_WhenExists()
    {
        var amenity = _fixture.Create<Amenity>();
        _amenityRepositoryMock.Setup(r => r.GetByIdAsync(amenity.Id))
            .ReturnsAsync(amenity);

        var result = await _service.GetByIdAsync(amenity.Id);

        Assert.Equal(amenity.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFound_WhenMissing()
    {
        _amenityRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Amenity?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnAmenity_WhenValid()
    {
        var amenity = _fixture.Create<Amenity>();

        _amenityValidatorMock.Setup(v => v.ValidateAsync(amenity, default))
            .ReturnsAsync(new ValidationResult());

        _amenityRepositoryMock.Setup(r => r.AddAsync(amenity))
            .ReturnsAsync(amenity);

        _amenityRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _service.AddAsync(amenity);

        Assert.Equal(amenity.Id, result.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var amenity = _fixture.Create<Amenity>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Field", "Invalid")
        });

        _amenityValidatorMock.Setup(v => v.ValidateAsync(amenity, default))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _service.AddAsync(amenity));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowCreationException_WhenRepositoryFails()
    {
        var amenity = _fixture.Create<Amenity>();

        _amenityValidatorMock.Setup(v => v.ValidateAsync(amenity, default))
            .ReturnsAsync(new ValidationResult());

        _amenityRepositoryMock.Setup(r => r.AddAsync(amenity))
            .ReturnsAsync((Amenity?)null!);

        _amenityRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<CreationException>(() => _service.AddAsync(amenity));
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenValid()
    {
        var amenity = _fixture.Create<Amenity>();

        _amenityValidatorMock
            .Setup(v => v.ValidateAsync(amenity, default))
            .ReturnsAsync(new ValidationResult());

        _amenityRepositoryMock
            .Setup(r => r.UpdateAsync(amenity))
            .ReturnsAsync(true);

        _amenityRepositoryMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await _service.UpdateAsync(amenity);

        _amenityValidatorMock.Verify(v => v.ValidateAsync(amenity, default), Times.Once);
        _amenityRepositoryMock.Verify(r => r.UpdateAsync(amenity), Times.Once);
        _amenityRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var amenity = _fixture.Create<Amenity>();

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required")
        });

        _amenityValidatorMock.Setup(v => v.ValidateAsync(amenity, default))
            .ReturnsAsync(validationResult);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(amenity));

        Assert.Contains("Name is required", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenRepoReturnsFalse()
    {
        var amenity = _fixture.Create<Amenity>();

        _amenityValidatorMock.Setup(v => v.ValidateAsync(amenity, default))
            .ReturnsAsync(new ValidationResult());

        _amenityRepositoryMock.Setup(r => r.UpdateAsync(amenity))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(amenity));
    }

    [Fact]
    public async Task DeleteAsync_ShouldSucceed_WhenExists()
    {
        var id = _fixture.Create<int>();

        _amenityRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
        _amenityRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id);

        _amenityRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _amenityRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFound_WhenMissing()
    {
        var id = _fixture.Create<int>();

        _amenityRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id));

        _amenityRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _amenityRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
