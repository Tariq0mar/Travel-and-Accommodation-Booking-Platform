using TABP.Domain.Entities;
using TABP.Domain.QueryFilters;

namespace TABP.Domain.Interfaces.Services;

public interface ICartItemSerivce
{
    Task<CartItem> GetByIdAsync(Guid id);
    Task<IEnumerable<CartItem>> GetAllAsync(CartItemFilter queryFilter);
    Task<CartItem> AddAsync(CartItem cartItem);
    Task UpdateAsync(CartItem cartItem);
    Task DeleteAsync(Guid id);
}