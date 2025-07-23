using FluentValidation;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories.CRUD;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services.CRUD;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IValidator<Payment> _paymentValidator;

    public PaymentService(
        IPaymentRepository paymentRepository,
        IValidator<Payment> paymentValidator)
    {
        _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
        _paymentValidator = paymentValidator ?? throw new ArgumentNullException(nameof(paymentValidator));
    }

    public async Task<Payment> GetByIdAsync(int id)
    {
        var payment = await _paymentRepository.GetByIdAsync(id);

        if (payment is null)
        {
            throw new NotFoundException($"Payment with Id = {id} not found.");
        }

        return payment;
    }

    public async Task<IEnumerable<Payment>> GetAllAsync(PaymentFilter queryFilter)
    {
        return await _paymentRepository.GetAllAsync(queryFilter);
    }

    public async Task<Payment> AddAsync(Payment payment)
    {
        var validation = await _paymentValidator.ValidateAsync(payment);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid payment: {errors}");
        }

        var addedPayment = await _paymentRepository.AddAsync(payment);

        if (addedPayment is null)
        {
            throw new CreationException($"Payment could not be created.");
        }

        await _paymentRepository.SaveChangesAsync();

        return addedPayment;
    }

    public async Task UpdateAsync(Payment payment)
    {
        var validation = await _paymentValidator.ValidateAsync(payment);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid payment update: {errors}");
        }

        var success = await _paymentRepository.UpdateAsync(payment);
        if (!success)
        {
            throw new NotFoundException($"Payment with Id = {payment.Id} not found.");
        }

        await _paymentRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var success = await _paymentRepository.DeleteAsync(id);
        if (!success)
        {
            throw new NotFoundException($"Payment with Id = {id} not found.");
        }

        await _paymentRepository.SaveChangesAsync();
    }
}
