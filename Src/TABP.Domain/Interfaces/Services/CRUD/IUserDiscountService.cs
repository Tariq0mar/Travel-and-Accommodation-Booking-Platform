using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services.CRUD;

public interface IUserDiscountService
{
    Task<UserDiscount> GetByIdAsync(int id);
    Task<IEnumerable<UserDiscount>> GetAllAsync(UserDiscountFilter queryFilter);
    Task<UserDiscount> AddAsync(UserDiscount userDiscount);
    Task UpdateAsync(UserDiscount userDiscount);
    Task DeleteAsync(int id);
}