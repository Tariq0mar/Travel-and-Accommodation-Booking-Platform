using TABP.Domain.Entities;
using TABP.Domain.Filters;

namespace TABP.Domain.Interfaces;

public interface IUserDiscountRepository
{
    Task<UserDiscount?> GetByIdAsync(Guid id);
    Task<IEnumerable<UserDiscount>> GetAllAsync(UserDiscountFilter filter);
    Task<UserDiscount> AddAsync(UserDiscount userDiscount);
    Task<bool> UpdateAsync(UserDiscount userDiscount);
    Task<bool> DeleteAsync(UserDiscount userDiscount);
}