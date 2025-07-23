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

public class LocationServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<ILocationRepository> _locationRepositoryMock;
    private readonly Mock<IValidator<Location>> _locationValidatorMock;
    private readonly LocationService _service;

    public LocationServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _locationRepositoryMock = new Mock<ILocationRepository>();
        _locationValidatorMock = new Mock<IValidator<Location>>();

        _service = new LocationService(
            _locationRepositoryMock.Object,
            _locationValidatorMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnLocation_WhenExists()
    {
        var location = _fixture.Create<Location>();
        _locationRepositoryMock.Setup(r => r.GetByIdAsync(location.Id))
            .ReturnsAsync(location);

        var result = await _service.GetByIdAsync(location.Id);

        Assert.Equal(location.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFound_WhenMissing()
    {
        _locationRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Location?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnLocation_WhenValid()
    {
        var location = _fixture.Create<Location>();

        _locationValidatorMock.Setup(v => v.ValidateAsync(location, default))
            .ReturnsAsync(new ValidationResult());

        _locationRepositoryMock.Setup(r => r.AddAsync(location))
            .ReturnsAsync(location);

        _locationRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _service.AddAsync(location);

        Assert.Equal(location.Id, result.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var location = _fixture.Create<Location>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Field", "Invalid")
        });

        _locationValidatorMock.Setup(v => v.ValidateAsync(location, default))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _service.AddAsync(location));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowCreationException_WhenRepositoryFails()
    {
        var location = _fixture.Create<Location>();

        _locationValidatorMock.Setup(v => v.ValidateAsync(location, default))
            .ReturnsAsync(new ValidationResult());

        _locationRepositoryMock.Setup(r => r.AddAsync(location))
            .ReturnsAsync((Location?)null!);

        _locationRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<CreationException>(() => _service.AddAsync(location));
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenValid()
    {
        var location = _fixture.Create<Location>();

        _locationValidatorMock
            .Setup(v => v.ValidateAsync(location, default))
            .ReturnsAsync(new ValidationResult());

        _locationRepositoryMock
            .Setup(r => r.UpdateAsync(location))
            .ReturnsAsync(true);

        _locationRepositoryMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await _service.UpdateAsync(location);

        _locationValidatorMock.Verify(v => v.ValidateAsync(location, default), Times.Once);
        _locationRepositoryMock.Verify(r => r.UpdateAsync(location), Times.Once);
        _locationRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var location = _fixture.Create<Location>();

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required")
        });

        _locationValidatorMock.Setup(v => v.ValidateAsync(location, default))
            .ReturnsAsync(validationResult);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(location));

        Assert.Contains("Name is required", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenRepoReturnsFalse()
    {
        var location = _fixture.Create<Location>();

        _locationValidatorMock.Setup(v => v.ValidateAsync(location, default))
            .ReturnsAsync(new ValidationResult());

        _locationRepositoryMock.Setup(r => r.UpdateAsync(location))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(location));
    }

    [Fact]
    public async Task DeleteAsync_ShouldSucceed_WhenExists()
    {
        var id = _fixture.Create<int>();

        _locationRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
        _locationRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id);

        _locationRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _locationRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFound_WhenMissing()
    {
        var id = _fixture.Create<int>();

        _locationRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id));

        _locationRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _locationRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
