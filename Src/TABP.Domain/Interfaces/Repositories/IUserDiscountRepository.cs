using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface IUserDiscountRepository
{
    Task<UserDiscount?> GetByIdAsync(int id);
    Task<IEnumerable<UserDiscount>> GetAllAsync(UserDiscountFilter filter);
    Task<UserDiscount> AddAsync(UserDiscount userDiscount);
    Task<bool> UpdateAsync(UserDiscount userDiscount);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}