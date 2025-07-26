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

public class GalleryServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IGalleryRepository> _galleryRepositoryMock;
    private readonly Mock<IValidator<Gallery>> _galleryValidatorMock;
    private readonly GalleryService _service;

    public GalleryServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _galleryRepositoryMock = new Mock<IGalleryRepository>();
        _galleryValidatorMock = new Mock<IValidator<Gallery>>();

        _service = new GalleryService(
            _galleryRepositoryMock.Object,
            _galleryValidatorMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnGallery_WhenExists()
    {
        var gallery = _fixture.Create<Gallery>();
        _galleryRepositoryMock.Setup(r => r.GetByIdAsync(gallery.Id))
            .ReturnsAsync(gallery);

        var result = await _service.GetByIdAsync(gallery.Id);

        Assert.Equal(gallery.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFound_WhenMissing()
    {
        _galleryRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Gallery?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnGallery_WhenValid()
    {
        var gallery = _fixture.Create<Gallery>();

        _galleryValidatorMock.Setup(v => v.ValidateAsync(gallery, default))
            .ReturnsAsync(new ValidationResult());

        _galleryRepositoryMock.Setup(r => r.AddAsync(gallery))
            .ReturnsAsync(gallery);

        _galleryRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _service.AddAsync(gallery);

        Assert.Equal(gallery.Id, result.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var gallery = _fixture.Create<Gallery>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Field", "Invalid")
        });

        _galleryValidatorMock.Setup(v => v.ValidateAsync(gallery, default))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _service.AddAsync(gallery));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowCreationException_WhenRepositoryFails()
    {
        var gallery = _fixture.Create<Gallery>();

        _galleryValidatorMock.Setup(v => v.ValidateAsync(gallery, default))
            .ReturnsAsync(new ValidationResult());

        _galleryRepositoryMock.Setup(r => r.AddAsync(gallery))
            .ReturnsAsync((Gallery?)null!);

        _galleryRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<CreationException>(() => _service.AddAsync(gallery));
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenValid()
    {
        var gallery = _fixture.Create<Gallery>();

        _galleryValidatorMock
            .Setup(v => v.ValidateAsync(gallery, default))
            .ReturnsAsync(new ValidationResult());

        _galleryRepositoryMock
            .Setup(r => r.UpdateAsync(gallery))
            .ReturnsAsync(true);

        _galleryRepositoryMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await _service.UpdateAsync(gallery);

        _galleryValidatorMock.Verify(v => v.ValidateAsync(gallery, default), Times.Once);
        _galleryRepositoryMock.Verify(r => r.UpdateAsync(gallery), Times.Once);
        _galleryRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var gallery = _fixture.Create<Gallery>();

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required")
        });

        _galleryValidatorMock.Setup(v => v.ValidateAsync(gallery, default))
            .ReturnsAsync(validationResult);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(gallery));

        Assert.Contains("Name is required", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenRepoReturnsFalse()
    {
        var gallery = _fixture.Create<Gallery>();

        _galleryValidatorMock.Setup(v => v.ValidateAsync(gallery, default))
            .ReturnsAsync(new ValidationResult());

        _galleryRepositoryMock.Setup(r => r.UpdateAsync(gallery))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(gallery));
    }

    [Fact]
    public async Task DeleteAsync_ShouldSucceed_WhenExists()
    {
        var id = _fixture.Create<int>();

        _galleryRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
        _galleryRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id);

        _galleryRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _galleryRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFound_WhenMissing()
    {
        var id = _fixture.Create<int>();

        _galleryRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id));

        _galleryRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _galleryRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
