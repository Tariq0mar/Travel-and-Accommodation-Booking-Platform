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

public class UserServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IValidator<User>> _userValidatorMock;
    private readonly UserService _service;

    public UserServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _userRepositoryMock = new Mock<IUserRepository>();
        _userValidatorMock = new Mock<IValidator<User>>();

        _service = new UserService(
            _userRepositoryMock.Object,
            _userValidatorMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnUser_WhenExists()
    {
        var user = _fixture.Create<User>();
        _userRepositoryMock.Setup(r => r.GetByIdAsync(user.Id))
            .ReturnsAsync(user);

        var result = await _service.GetByIdAsync(user.Id);

        Assert.Equal(user.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFound_WhenMissing()
    {
        _userRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((User?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnUser_WhenValid()
    {
        var user = _fixture.Create<User>();

        _userValidatorMock.Setup(v => v.ValidateAsync(user, default))
            .ReturnsAsync(new ValidationResult());

        _userRepositoryMock.Setup(r => r.AddAsync(user))
            .ReturnsAsync(user);

        _userRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _service.AddAsync(user);

        Assert.Equal(user.Id, result.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var user = _fixture.Create<User>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Field", "Invalid")
        });

        _userValidatorMock.Setup(v => v.ValidateAsync(user, default))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _service.AddAsync(user));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowCreationException_WhenRepositoryFails()
    {
        var user = _fixture.Create<User>();

        _userValidatorMock.Setup(v => v.ValidateAsync(user, default))
            .ReturnsAsync(new ValidationResult());

        _userRepositoryMock.Setup(r => r.AddAsync(user))
            .ReturnsAsync((User?)null!);

        _userRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<CreationException>(() => _service.AddAsync(user));
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenValid()
    {
        var user = _fixture.Create<User>();

        _userValidatorMock
            .Setup(v => v.ValidateAsync(user, default))
            .ReturnsAsync(new ValidationResult());

        _userRepositoryMock
            .Setup(r => r.UpdateAsync(user))
            .ReturnsAsync(true);

        _userRepositoryMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await _service.UpdateAsync(user);

        _userValidatorMock.Verify(v => v.ValidateAsync(user, default), Times.Once);
        _userRepositoryMock.Verify(r => r.UpdateAsync(user), Times.Once);
        _userRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var user = _fixture.Create<User>();

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Email", "Email is required")
        });

        _userValidatorMock.Setup(v => v.ValidateAsync(user, default))
            .ReturnsAsync(validationResult);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(user));

        Assert.Contains("Email is required", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenRepoReturnsFalse()
    {
        var user = _fixture.Create<User>();

        _userValidatorMock.Setup(v => v.ValidateAsync(user, default))
            .ReturnsAsync(new ValidationResult());

        _userRepositoryMock.Setup(r => r.UpdateAsync(user))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(user));
    }

    [Fact]
    public async Task DeleteAsync_ShouldSucceed_WhenExists()
    {
        var id = _fixture.Create<int>();

        _userRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
        _userRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id);

        _userRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _userRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFound_WhenMissing()
    {
        var id = _fixture.Create<int>();

        _userRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id));

        _userRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _userRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
