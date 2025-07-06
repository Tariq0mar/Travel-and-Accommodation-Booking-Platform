using TABP.Domain.Entities;
using TABP.Domain.QueryFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface IReviewRepository
{
    Task<Review?> GetByIdAsync(Guid id);
    Task<IEnumerable<Review>> GetAllAsync(ReviewFilter filter);
    Task<Review> AddAsync(Review review);
    Task<bool> UpdateAsync(Review review);
    Task<bool> DeleteAsync(Guid id);
}