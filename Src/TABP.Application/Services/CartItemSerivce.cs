using FluentValidation;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services;

public class CartItemService : ICartItemService 
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IValidator<CartItem> _cartItemValidator;

    public CartItemService(
        ICartItemRepository cartItemRepository,
        IValidator<CartItem> cartItemValidator)
    {
        _cartItemRepository = cartItemRepository ?? throw new ArgumentNullException(nameof(cartItemRepository));
        _cartItemValidator = cartItemValidator ?? throw new ArgumentNullException(nameof(cartItemValidator));
    }

    public async Task<CartItem> GetByIdAsync(int id)
    {
        var cartItem = await _cartItemRepository.GetByIdAsync(id);

        if (cartItem is null)
        {
            throw new NotFoundException($"CartItem with Id = {id} not found.");
        }

        return cartItem;
    }

    public async Task<IEnumerable<CartItem>> GetAllAsync(CartItemFilter queryFilter)
    {
        return await _cartItemRepository.GetAllAsync(queryFilter);
    }

    public async Task<CartItem> AddAsync(CartItem cartItem)
    {
        var validation = await _cartItemValidator.ValidateAsync(cartItem);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid cart item: {errors}");
        }

        var addedCartItem = await _cartItemRepository.AddAsync(cartItem);

        if (addedCartItem is null)
        {
            throw new CreationException($"CartItem could not be created.");
        }

        await _cartItemRepository.SaveChangesAsync();

        return addedCartItem;
    }

    public async Task UpdateAsync(CartItem cartItem)
    {
        var validation = await _cartItemValidator.ValidateAsync(cartItem);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid cart item update: {errors}");
        }

        var success = await _cartItemRepository.UpdateAsync(cartItem);
        if (!success)
        {
            throw new NotFoundException($"CartItem with Id = {cartItem.Id} not found.");
        }

        await _cartItemRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var success = await _cartItemRepository.DeleteAsync(id);
        if (!success)
        {
            throw new NotFoundException($"CartItem with Id = {id} not found.");
        }

        await _cartItemRepository.SaveChangesAsync();
    }
}
