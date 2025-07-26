using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface IDiscountRepository
{
    Task<Discount?> GetByIdAsync(int id);
    Task<IEnumerable<Discount>> GetAllAsync(DiscountFilter filter);
    Task<Discount> AddAsync(Discount discount);
    Task<bool> UpdateAsync(Discount discount);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}