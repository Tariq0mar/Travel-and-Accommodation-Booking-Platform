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

public class RoomServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IRoomRepository> _roomRepositoryMock;
    private readonly Mock<IValidator<Room>> _roomValidatorMock;
    private readonly RoomService _service;

    public RoomServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _roomRepositoryMock = new Mock<IRoomRepository>();
        _roomValidatorMock = new Mock<IValidator<Room>>();

        _service = new RoomService(
            _roomRepositoryMock.Object,
            _roomValidatorMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnRoom_WhenExists()
    {
        var room = _fixture.Create<Room>();
        _roomRepositoryMock.Setup(r => r.GetByIdAsync(room.Id))
            .ReturnsAsync(room);

        var result = await _service.GetByIdAsync(room.Id);

        Assert.Equal(room.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFound_WhenMissing()
    {
        _roomRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Room?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnRoom_WhenValid()
    {
        var room = _fixture.Create<Room>();

        _roomValidatorMock.Setup(v => v.ValidateAsync(room, default))
            .ReturnsAsync(new ValidationResult());

        _roomRepositoryMock.Setup(r => r.AddAsync(room))
            .ReturnsAsync(room);

        _roomRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _service.AddAsync(room);

        Assert.Equal(room.Id, result.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var room = _fixture.Create<Room>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Field", "Invalid")
        });

        _roomValidatorMock.Setup(v => v.ValidateAsync(room, default))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _service.AddAsync(room));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowCreationException_WhenRepositoryFails()
    {
        var room = _fixture.Create<Room>();

        _roomValidatorMock.Setup(v => v.ValidateAsync(room, default))
            .ReturnsAsync(new ValidationResult());

        _roomRepositoryMock.Setup(r => r.AddAsync(room))
            .ReturnsAsync((Room?)null!);

        _roomRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<CreationException>(() => _service.AddAsync(room));
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenValid()
    {
        var room = _fixture.Create<Room>();

        _roomValidatorMock
            .Setup(v => v.ValidateAsync(room, default))
            .ReturnsAsync(new ValidationResult());

        _roomRepositoryMock
            .Setup(r => r.UpdateAsync(room))
            .ReturnsAsync(true);

        _roomRepositoryMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await _service.UpdateAsync(room);

        _roomValidatorMock.Verify(v => v.ValidateAsync(room, default), Times.Once);
        _roomRepositoryMock.Verify(r => r.UpdateAsync(room), Times.Once);
        _roomRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var room = _fixture.Create<Room>();

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required")
        });

        _roomValidatorMock.Setup(v => v.ValidateAsync(room, default))
            .ReturnsAsync(validationResult);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(room));

        Assert.Contains("Name is required", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenRepoReturnsFalse()
    {
        var room = _fixture.Create<Room>();

        _roomValidatorMock.Setup(v => v.ValidateAsync(room, default))
            .ReturnsAsync(new ValidationResult());

        _roomRepositoryMock.Setup(r => r.UpdateAsync(room))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(room));
    }

    [Fact]
    public async Task DeleteAsync_ShouldSucceed_WhenExists()
    {
        var id = _fixture.Create<int>();

        _roomRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
        _roomRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id);

        _roomRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _roomRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFound_WhenMissing()
    {
        var id = _fixture.Create<int>();

        _roomRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id));

        _roomRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _roomRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
