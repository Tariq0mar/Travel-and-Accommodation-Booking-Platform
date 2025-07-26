using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface IReviewRepository
{
    Task<Review?> GetByIdAsync(int id);
    Task<IEnumerable<Review>> GetByUserIdAsync(int id);
    Task<IEnumerable<Review>> GetAllAsync(ReviewFilter filter);
    Task<Review> AddAsync(Review review);
    Task<bool> UpdateAsync(Review review);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}