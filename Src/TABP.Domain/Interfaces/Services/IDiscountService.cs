using TABP.Domain.Entities;
using TABP.Domain.QueryFilters;

namespace TABP.Domain.Interfaces.Services;

public interface IDiscountService
{
    Task<Discount> GetByIdAsync(Guid id);
    Task<IEnumerable<Discount>> GetAllAsync(DiscountFilter queryFilter);
    Task<Discount> AddAsync(Discount discount);
    Task UpdateAsync(Discount discount);
    Task DeleteAsync(Guid id);
}