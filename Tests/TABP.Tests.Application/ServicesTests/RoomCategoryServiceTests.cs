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

public class RoomCategoryServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IRoomCategoryRepository> _roomCategoryRepositoryMock;
    private readonly Mock<IValidator<RoomCategory>> _roomCategoryValidatorMock;
    private readonly RoomCategoryService _service;

    public RoomCategoryServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _roomCategoryRepositoryMock = new Mock<IRoomCategoryRepository>();
        _roomCategoryValidatorMock = new Mock<IValidator<RoomCategory>>();

        _service = new RoomCategoryService(
            _roomCategoryRepositoryMock.Object,
            _roomCategoryValidatorMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnRoomCategory_WhenExists()
    {
        var roomCategory = _fixture.Create<RoomCategory>();
        _roomCategoryRepositoryMock.Setup(r => r.GetByIdAsync(roomCategory.Id))
            .ReturnsAsync(roomCategory);

        var result = await _service.GetByIdAsync(roomCategory.Id);

        Assert.Equal(roomCategory.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFound_WhenMissing()
    {
        _roomCategoryRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((RoomCategory?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnRoomCategory_WhenValid()
    {
        var roomCategory = _fixture.Create<RoomCategory>();

        _roomCategoryValidatorMock.Setup(v => v.ValidateAsync(roomCategory, default))
            .ReturnsAsync(new ValidationResult());

        _roomCategoryRepositoryMock.Setup(r => r.AddAsync(roomCategory))
            .ReturnsAsync(roomCategory);

        _roomCategoryRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _service.AddAsync(roomCategory);

        Assert.Equal(roomCategory.Id, result.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var roomCategory = _fixture.Create<RoomCategory>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Field", "Invalid")
        });

        _roomCategoryValidatorMock.Setup(v => v.ValidateAsync(roomCategory, default))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _service.AddAsync(roomCategory));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowCreationException_WhenRepositoryFails()
    {
        var roomCategory = _fixture.Create<RoomCategory>();

        _roomCategoryValidatorMock.Setup(v => v.ValidateAsync(roomCategory, default))
            .ReturnsAsync(new ValidationResult());

        _roomCategoryRepositoryMock.Setup(r => r.AddAsync(roomCategory))
            .ReturnsAsync((RoomCategory?)null!);

        _roomCategoryRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<CreationException>(() => _service.AddAsync(roomCategory));
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenValid()
    {
        var roomCategory = _fixture.Create<RoomCategory>();

        _roomCategoryValidatorMock
            .Setup(v => v.ValidateAsync(roomCategory, default))
            .ReturnsAsync(new ValidationResult());

        _roomCategoryRepositoryMock
            .Setup(r => r.UpdateAsync(roomCategory))
            .ReturnsAsync(true);

        _roomCategoryRepositoryMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await _service.UpdateAsync(roomCategory);

        _roomCategoryValidatorMock.Verify(v => v.ValidateAsync(roomCategory, default), Times.Once);
        _roomCategoryRepositoryMock.Verify(r => r.UpdateAsync(roomCategory), Times.Once);
        _roomCategoryRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var roomCategory = _fixture.Create<RoomCategory>();

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required")
        });

        _roomCategoryValidatorMock.Setup(v => v.ValidateAsync(roomCategory, default))
            .ReturnsAsync(validationResult);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(roomCategory));

        Assert.Contains("Name is required", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenRepoReturnsFalse()
    {
        var roomCategory = _fixture.Create<RoomCategory>();

        _roomCategoryValidatorMock.Setup(v => v.ValidateAsync(roomCategory, default))
            .ReturnsAsync(new ValidationResult());

        _roomCategoryRepositoryMock.Setup(r => r.UpdateAsync(roomCategory))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(roomCategory));
    }

    [Fact]
    public async Task DeleteAsync_ShouldSucceed_WhenExists()
    {
        var id = _fixture.Create<int>();

        _roomCategoryRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
        _roomCategoryRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id);

        _roomCategoryRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _roomCategoryRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFound_WhenMissing()
    {
        var id = _fixture.Create<int>();

        _roomCategoryRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id));

        _roomCategoryRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _roomCategoryRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
