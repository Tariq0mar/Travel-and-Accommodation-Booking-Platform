using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services.CRUD;

public interface IDiscountService
{
    Task<Discount> GetByIdAsync(int id);
    Task<IEnumerable<Discount>> GetAllAsync(DiscountFilter queryFilter);
    Task<Discount> AddAsync(Discount discount);
    Task UpdateAsync(Discount discount);
    Task DeleteAsync(int id);
}