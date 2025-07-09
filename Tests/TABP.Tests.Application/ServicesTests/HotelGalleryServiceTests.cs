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

public class HotelGalleryServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IHotelGalleryRepository> _hotelGalleryRepositoryMock;
    private readonly Mock<IValidator<HotelGallery>> _hotelGalleryValidatorMock;
    private readonly HotelGalleryService _service;

    public HotelGalleryServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _hotelGalleryRepositoryMock = new Mock<IHotelGalleryRepository>();
        _hotelGalleryValidatorMock = new Mock<IValidator<HotelGallery>>();

        _service = new HotelGalleryService(
            _hotelGalleryRepositoryMock.Object,
            _hotelGalleryValidatorMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnHotelGallery_WhenExists()
    {
        var hotelGallery = _fixture.Create<HotelGallery>();
        _hotelGalleryRepositoryMock.Setup(r => r.GetByIdAsync(hotelGallery.Id))
            .ReturnsAsync(hotelGallery);

        var result = await _service.GetByIdAsync(hotelGallery.Id);

        Assert.Equal(hotelGallery.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFound_WhenMissing()
    {
        _hotelGalleryRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((HotelGallery?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnHotelGallery_WhenValid()
    {
        var hotelGallery = _fixture.Create<HotelGallery>();

        _hotelGalleryValidatorMock.Setup(v => v.ValidateAsync(hotelGallery, default))
            .ReturnsAsync(new ValidationResult());

        _hotelGalleryRepositoryMock.Setup(r => r.AddAsync(hotelGallery))
            .ReturnsAsync(hotelGallery);

        _hotelGalleryRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _service.AddAsync(hotelGallery);

        Assert.Equal(hotelGallery.Id, result.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var hotelGallery = _fixture.Create<HotelGallery>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Field", "Invalid")
        });

        _hotelGalleryValidatorMock.Setup(v => v.ValidateAsync(hotelGallery, default))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _service.AddAsync(hotelGallery));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowCreationException_WhenRepositoryFails()
    {
        var hotelGallery = _fixture.Create<HotelGallery>();

        _hotelGalleryValidatorMock.Setup(v => v.ValidateAsync(hotelGallery, default))
            .ReturnsAsync(new ValidationResult());

        _hotelGalleryRepositoryMock.Setup(r => r.AddAsync(hotelGallery))
            .ReturnsAsync((HotelGallery?)null!);

        _hotelGalleryRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<CreationException>(() => _service.AddAsync(hotelGallery));
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenValid()
    {
        var hotelGallery = _fixture.Create<HotelGallery>();

        _hotelGalleryValidatorMock
            .Setup(v => v.ValidateAsync(hotelGallery, default))
            .ReturnsAsync(new ValidationResult());

        _hotelGalleryRepositoryMock
            .Setup(r => r.UpdateAsync(hotelGallery))
            .ReturnsAsync(true);

        _hotelGalleryRepositoryMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await _service.UpdateAsync(hotelGallery);

        _hotelGalleryValidatorMock.Verify(v => v.ValidateAsync(hotelGallery, default), Times.Once);
        _hotelGalleryRepositoryMock.Verify(r => r.UpdateAsync(hotelGallery), Times.Once);
        _hotelGalleryRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var hotelGallery = _fixture.Create<HotelGallery>();

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required")
        });

        _hotelGalleryValidatorMock.Setup(v => v.ValidateAsync(hotelGallery, default))
            .ReturnsAsync(validationResult);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(hotelGallery));

        Assert.Contains("Name is required", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenRepoReturnsFalse()
    {
        var hotelGallery = _fixture.Create<HotelGallery>();

        _hotelGalleryValidatorMock.Setup(v => v.ValidateAsync(hotelGallery, default))
            .ReturnsAsync(new ValidationResult());

        _hotelGalleryRepositoryMock.Setup(r => r.UpdateAsync(hotelGallery))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(hotelGallery));
    }

    [Fact]
    public async Task DeleteAsync_ShouldSucceed_WhenExists()
    {
        var id = _fixture.Create<int>();

        _hotelGalleryRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
        _hotelGalleryRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id);

        _hotelGalleryRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _hotelGalleryRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFound_WhenMissing()
    {
        var id = _fixture.Create<int>();

        _hotelGalleryRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id));

        _hotelGalleryRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _hotelGalleryRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
