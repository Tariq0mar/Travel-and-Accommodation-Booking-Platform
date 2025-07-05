using TABP.Domain.Entities;
using TABP.Domain.Filters;

namespace TABP.Domain.Interfaces;

public interface IDiscountRepository
{
    Task<Discount?> GetByIdAsync(Guid id);
    Task<IEnumerable<Discount>> GetAllAsync(DiscountFilter filter);
    Task<Discount> AddAsync(Discount discount);
    Task<bool> UpdateAsync(Discount discount);
    Task<bool> DeleteAsync(Discount discount);
}