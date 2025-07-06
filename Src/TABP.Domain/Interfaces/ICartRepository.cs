using TABP.Domain.Entities;
using TABP.Domain.Filters;

namespace TABP.Domain.Interfaces;

public interface ICartRepository
{
    Task<CartItem?> GetByIdAsync(Guid id);
    Task<IEnumerable<CartItem>> GetAllAsync(CartItemFilter filter);
    Task<CartItem> AddAsync(CartItem cart);
    Task<bool> UpdateAsync(CartItem cart);
    Task<bool> DeleteAsync(CartItem cart);
}