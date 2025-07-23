using FluentValidation;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories.CRUD;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services.CRUD;

public class DiscountService : IDiscountService
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IValidator<Discount> _discountValidator;

    public DiscountService(
        IDiscountRepository discountRepository,
        IValidator<Discount> discountValidator)
    {
        _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
        _discountValidator = discountValidator ?? throw new ArgumentNullException(nameof(discountValidator));
    }

    public async Task<Discount> GetByIdAsync(int id)
    {
        var discount = await _discountRepository.GetByIdAsync(id);

        if (discount is null)
        {
            throw new NotFoundException($"Discount with Id = {id} not found.");
        }

        return discount;
    }

    public async Task<IEnumerable<Discount>> GetAllAsync(DiscountFilter queryFilter)
    {
        return await _discountRepository.GetAllAsync(queryFilter);
    }

    public async Task<Discount> AddAsync(Discount discount)
    {
        var validation = await _discountValidator.ValidateAsync(discount);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid discount: {errors}");
        }

        var addedDiscount = await _discountRepository.AddAsync(discount);

        if (addedDiscount is null)
        {
            throw new CreationException($"Discount could not be created.");
        }

        await _discountRepository.SaveChangesAsync();

        return addedDiscount;
    }

    public async Task UpdateAsync(Discount discount)
    {
        var validation = await _discountValidator.ValidateAsync(discount);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid discount update: {errors}");
        }

        var success = await _discountRepository.UpdateAsync(discount);
        if (!success)
        {
            throw new NotFoundException($"Discount with Id = {discount.Id} not found.");
        }

        await _discountRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var success = await _discountRepository.DeleteAsync(id);
        if (!success)
        {
            throw new NotFoundException($"Discount with Id = {id} not found.");
        }

        await _discountRepository.SaveChangesAsync();
    }
}
