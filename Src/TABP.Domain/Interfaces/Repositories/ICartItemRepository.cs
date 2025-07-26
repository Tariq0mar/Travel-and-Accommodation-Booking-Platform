using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface ICartItemRepository
{
    Task<CartItem?> GetByIdAsync(int id);
    Task<IEnumerable<CartItem>> GetAllAsync(CartItemFilter filter);
    Task<CartItem> AddAsync(CartItem cart);
    Task<bool> UpdateAsync(CartItem cart);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}