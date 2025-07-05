using TABP.Domain.Entities;
using TABP.Domain.Filters;

namespace TABP.Domain.Interfaces;

public interface ICartRepository
{
    Task<Cart?> GetByIdAsync(Guid id);
    Task<IEnumerable<Cart>> GetAllAsync(CartFilter filter);
    Task<Cart> AddAsync(Cart cart);
    Task<bool> UpdateAsync(Cart cart);
    Task<bool> DeleteAsync(Cart cart);
}