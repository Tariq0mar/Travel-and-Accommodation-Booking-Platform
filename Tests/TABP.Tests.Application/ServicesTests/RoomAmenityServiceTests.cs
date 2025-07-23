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

public class RoomAmenityServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IRoomAmenityRepository> _roomAmenityRepositoryMock;
    private readonly Mock<IValidator<RoomAmenity>> _roomAmenityValidatorMock;
    private readonly RoomAmenityService _service;

    public RoomAmenityServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _roomAmenityRepositoryMock = new Mock<IRoomAmenityRepository>();
        _roomAmenityValidatorMock = new Mock<IValidator<RoomAmenity>>();

        _service = new RoomAmenityService(
            _roomAmenityRepositoryMock.Object,
            _roomAmenityValidatorMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnRoomAmenity_WhenExists()
    {
        var roomAmenity = _fixture.Create<RoomAmenity>();
        _roomAmenityRepositoryMock.Setup(r => r.GetByIdAsync(roomAmenity.Id))
            .ReturnsAsync(roomAmenity);

        var result = await _service.GetByIdAsync(roomAmenity.Id);

        Assert.Equal(roomAmenity.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFound_WhenMissing()
    {
        _roomAmenityRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((RoomAmenity?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnRoomAmenity_WhenValid()
    {
        var roomAmenity = _fixture.Create<RoomAmenity>();

        _roomAmenityValidatorMock.Setup(v => v.ValidateAsync(roomAmenity, default))
            .ReturnsAsync(new ValidationResult());

        _roomAmenityRepositoryMock.Setup(r => r.AddAsync(roomAmenity))
            .ReturnsAsync(roomAmenity);

        _roomAmenityRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _service.AddAsync(roomAmenity);

        Assert.Equal(roomAmenity.Id, result.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var roomAmenity = _fixture.Create<RoomAmenity>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Field", "Invalid")
        });

        _roomAmenityValidatorMock.Setup(v => v.ValidateAsync(roomAmenity, default))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _service.AddAsync(roomAmenity));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowCreationException_WhenRepositoryFails()
    {
        var roomAmenity = _fixture.Create<RoomAmenity>();

        _roomAmenityValidatorMock.Setup(v => v.ValidateAsync(roomAmenity, default))
            .ReturnsAsync(new ValidationResult());

        _roomAmenityRepositoryMock.Setup(r => r.AddAsync(roomAmenity))
            .ReturnsAsync((RoomAmenity?)null!);

        _roomAmenityRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<CreationException>(() => _service.AddAsync(roomAmenity));
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenValid()
    {
        var roomAmenity = _fixture.Create<RoomAmenity>();

        _roomAmenityValidatorMock
            .Setup(v => v.ValidateAsync(roomAmenity, default))
            .ReturnsAsync(new ValidationResult());

        _roomAmenityRepositoryMock
            .Setup(r => r.UpdateAsync(roomAmenity))
            .ReturnsAsync(true);

        _roomAmenityRepositoryMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await _service.UpdateAsync(roomAmenity);

        _roomAmenityValidatorMock.Verify(v => v.ValidateAsync(roomAmenity, default), Times.Once);
        _roomAmenityRepositoryMock.Verify(r => r.UpdateAsync(roomAmenity), Times.Once);
        _roomAmenityRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var roomAmenity = _fixture.Create<RoomAmenity>();

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required")
        });

        _roomAmenityValidatorMock.Setup(v => v.ValidateAsync(roomAmenity, default))
            .ReturnsAsync(validationResult);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(roomAmenity));

        Assert.Contains("Name is required", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenRepoReturnsFalse()
    {
        var roomAmenity = _fixture.Create<RoomAmenity>();

        _roomAmenityValidatorMock.Setup(v => v.ValidateAsync(roomAmenity, default))
            .ReturnsAsync(new ValidationResult());

        _roomAmenityRepositoryMock.Setup(r => r.UpdateAsync(roomAmenity))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(roomAmenity));
    }

    [Fact]
    public async Task DeleteAsync_ShouldSucceed_WhenExists()
    {
        var id = _fixture.Create<int>();

        _roomAmenityRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
        _roomAmenityRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id);

        _roomAmenityRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _roomAmenityRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFound_WhenMissing()
    {
        var id = _fixture.Create<int>();

        _roomAmenityRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id));

        _roomAmenityRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _roomAmenityRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
