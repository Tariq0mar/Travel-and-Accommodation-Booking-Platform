using FluentValidation;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services;

public class UserDiscountService : IUserDiscountService
{
    private readonly IUserDiscountRepository _userDiscountRepository;
    private readonly IValidator<UserDiscount> _userDiscountValidator;

    public UserDiscountService(
        IUserDiscountRepository userDiscountRepository,
        IValidator<UserDiscount> userDiscountValidator)
    {
        _userDiscountRepository = userDiscountRepository ?? throw new ArgumentNullException(nameof(userDiscountRepository));
        _userDiscountValidator = userDiscountValidator ?? throw new ArgumentNullException(nameof(userDiscountValidator));
    }

    public async Task<UserDiscount> GetByIdAsync(int id)
    {
        var userDiscount = await _userDiscountRepository.GetByIdAsync(id);

        if (userDiscount is null)
        {
            throw new NotFoundException($"UserDiscount with Id = {id} not found.");
        }

        return userDiscount;
    }

    public async Task<IEnumerable<UserDiscount>> GetAllAsync(UserDiscountFilter queryFilter)
    {
        return await _userDiscountRepository.GetAllAsync(queryFilter);
    }

    public async Task<UserDiscount> AddAsync(UserDiscount userDiscount)
    {
        var validation = await _userDiscountValidator.ValidateAsync(userDiscount);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid user discount: {errors}");
        }

        var addedUserDiscount = await _userDiscountRepository.AddAsync(userDiscount);

        if (addedUserDiscount is null)
        {
            throw new CreationException($"UserDiscount could not be created.");
        }

        await _userDiscountRepository.SaveChangesAsync();

        return addedUserDiscount;
    }

    public async Task UpdateAsync(UserDiscount userDiscount)
    {
        var validation = await _userDiscountValidator.ValidateAsync(userDiscount);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid user discount update: {errors}");
        }

        var success = await _userDiscountRepository.UpdateAsync(userDiscount);
        if (!success)
        {
            throw new NotFoundException($"UserDiscount with Id = {userDiscount.Id} not found.");
        }

        await _userDiscountRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var success = await _userDiscountRepository.DeleteAsync(id);
        if (!success)
        {
            throw new NotFoundException($"UserDiscount with Id = {id} not found.");
        }

        await _userDiscountRepository.SaveChangesAsync();
    }
}
