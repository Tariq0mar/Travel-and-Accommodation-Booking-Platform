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

public class PaymentServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IPaymentRepository> _paymentRepositoryMock;
    private readonly Mock<IValidator<Payment>> _paymentValidatorMock;
    private readonly PaymentService _service;

    public PaymentServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _paymentRepositoryMock = new Mock<IPaymentRepository>();
        _paymentValidatorMock = new Mock<IValidator<Payment>>();

        _service = new PaymentService(
            _paymentRepositoryMock.Object,
            _paymentValidatorMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnPayment_WhenExists()
    {
        var payment = _fixture.Create<Payment>();
        _paymentRepositoryMock.Setup(r => r.GetByIdAsync(payment.Id))
            .ReturnsAsync(payment);

        var result = await _service.GetByIdAsync(payment.Id);

        Assert.Equal(payment.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFound_WhenMissing()
    {
        _paymentRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Payment?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(1));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnPayment_WhenValid()
    {
        var payment = _fixture.Create<Payment>();

        _paymentValidatorMock.Setup(v => v.ValidateAsync(payment, default))
            .ReturnsAsync(new ValidationResult());

        _paymentRepositoryMock.Setup(r => r.AddAsync(payment))
            .ReturnsAsync(payment);

        _paymentRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _service.AddAsync(payment);

        Assert.Equal(payment.Id, result.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var payment = _fixture.Create<Payment>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Field", "Invalid")
        });

        _paymentValidatorMock.Setup(v => v.ValidateAsync(payment, default))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _service.AddAsync(payment));
    }

    [Fact]
    public async Task AddAsync_ShouldThrowCreationException_WhenRepositoryFails()
    {
        var payment = _fixture.Create<Payment>();

        _paymentValidatorMock.Setup(v => v.ValidateAsync(payment, default))
            .ReturnsAsync(new ValidationResult());

        _paymentRepositoryMock.Setup(r => r.AddAsync(payment))
            .ReturnsAsync((Payment?)null!);

        _paymentRepositoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<CreationException>(() => _service.AddAsync(payment));
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenValid()
    {
        var payment = _fixture.Create<Payment>();

        _paymentValidatorMock
            .Setup(v => v.ValidateAsync(payment, default))
            .ReturnsAsync(new ValidationResult());

        _paymentRepositoryMock
            .Setup(r => r.UpdateAsync(payment))
            .ReturnsAsync(true);

        _paymentRepositoryMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        await _service.UpdateAsync(payment);

        _paymentValidatorMock.Verify(v => v.ValidateAsync(payment, default), Times.Once);
        _paymentRepositoryMock.Verify(r => r.UpdateAsync(payment), Times.Once);
        _paymentRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowValidationException_WhenInvalid()
    {
        var payment = _fixture.Create<Payment>();

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Amount", "Amount is required")
        });

        _paymentValidatorMock.Setup(v => v.ValidateAsync(payment, default))
            .ReturnsAsync(validationResult);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateAsync(payment));

        Assert.Contains("Amount is required", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenRepoReturnsFalse()
    {
        var payment = _fixture.Create<Payment>();

        _paymentValidatorMock.Setup(v => v.ValidateAsync(payment, default))
            .ReturnsAsync(new ValidationResult());

        _paymentRepositoryMock.Setup(r => r.UpdateAsync(payment))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(payment));
    }

    [Fact]
    public async Task DeleteAsync_ShouldSucceed_WhenExists()
    {
        var id = _fixture.Create<int>();

        _paymentRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
        _paymentRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id);

        _paymentRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _paymentRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFound_WhenMissing()
    {
        var id = _fixture.Create<int>();

        _paymentRepositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id));

        _paymentRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        _paymentRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}
