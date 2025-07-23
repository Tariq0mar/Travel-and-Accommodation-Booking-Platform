using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services.CRUD;

public interface ICartItemService
{
    Task<CartItem> GetByIdAsync(int id);
    Task<IEnumerable<CartItem>> GetAllAsync(CartItemFilter queryFilter);
    Task<CartItem> AddAsync(CartItem cartItem);
    Task UpdateAsync(CartItem cartItem);
    Task DeleteAsync(int id);
}