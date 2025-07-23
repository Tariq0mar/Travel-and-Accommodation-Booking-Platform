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

public class RoomGalleryServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IRoomGalleryRepository> _roomGalleryRepositoryMock;
    private readonly Mock<IValidator<RoomGallery>> _roomGalleryValidatorMock;
    private readonly RoomGalleryService _service;

    public RoomGalleryServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _roomGalleryRepositoryMock = new Mock<IRoomGalleryRepository>();
        _roomGalleryValidatorMock = new Mock<IValidator<RoomGallery>>();

        _service = new RoomGalleryService(
            _roomGalleryRepositoryMock.Object,
            _roomGalleryValidatorMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnRoomGallery_WhenExists()
    {
        var gallery = _fixture.Create<RoomGallery>();
        _roomGalleryRepositoryMock.Setup(r => r.GetByIdAsync(gallery.Id))
            .ReturnsAsync(gallery);

        var result = await _service.GetByIdAsync(gallery.Id);

        Assert.Equal(gallery.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFound_WhenMissing()
    {
        _roomGalleryRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((RoomGallery?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnRoomGallery_WhenValid()
    {
        var gallery = _fixture.Create<RoomGallery>();

        _roomGalleryValidatorMock.Setup(v => v.ValidateAsync(gallery, default))
            .ReturnsAsync(new ValidationResult());

        _roomGalleryRepositoryMock.Setup(r => r.AddAsync(gallery))
            .ReturnsAsync(gallery);

        _roomGalleryRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _service.AddAsync(gallery);

        Assert.Equal(gallery.Id, result.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var gallery = _fixture.Create<RoomGallery>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Field", "Invalid")
        });

        _roomGalleryValidatorMock.Setup(v => v.ValidateAsync(gallery, default))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _service.AddAsync(gallery));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowCreationException_WhenRepositoryFails()
    {
        var gallery = _fixture.Create<RoomGallery>();

        _roomGalleryValidatorMock.Setup(v => v.ValidateAsync(gallery, default))
            .ReturnsAsync(new ValidationResult());

        _roomGalleryRepositoryMock.Setup(r => r.AddAsync(gallery))
            .ReturnsAsync((RoomGallery?)null!);

        _roomGalleryRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<CreationException>(() => _service.AddAsync(gallery));
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenValid()
    {
        var gallery = _fixture.Create<RoomGallery>();

        _roomGalleryValidatorMock
            .Setup(v => v.ValidateAsync(gallery, default))
            .ReturnsAsync(new ValidationResult());

        _roomGalleryRepositoryMock
            .Setup(r => r.UpdateAsync(gallery))
            .ReturnsAsync(true);

        _roomGalleryRepositoryMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await _service.UpdateAsync(gallery);

        _roomGalleryValidatorMock.Verify(v => v.ValidateAsync(gallery, default), Times.Once);
        _roomGalleryRepositoryMock.Verify(r => r.UpdateAsync(gallery), Times.Once);
        _roomGalleryRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var gallery = _fixture.Create<RoomGallery>();

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("ImageUrl", "ImageUrl is required")
        });

        _roomGalleryValidatorMock.Setup(v => v.ValidateAsync(gallery, default))
            .ReturnsAsync(validationResult);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(gallery));

        Assert.Contains("ImageUrl is required", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenRepoReturnsFalse()
    {
        var gallery = _fixture.Create<RoomGallery>();

        _roomGalleryValidatorMock.Setup(v => v.ValidateAsync(gallery, default))
            .ReturnsAsync(new ValidationResult());

        _roomGalleryRepositoryMock.Setup(r => r.UpdateAsync(gallery))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(gallery));
    }

    [Fact]
    public async Task DeleteAsync_ShouldSucceed_WhenExists()
    {
        var id = _fixture.Create<int>();

        _roomGalleryRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
        _roomGalleryRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id);

        _roomGalleryRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _roomGalleryRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFound_WhenMissing()
    {
        var id = _fixture.Create<int>();

        _roomGalleryRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id));

        _roomGalleryRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _roomGalleryRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
