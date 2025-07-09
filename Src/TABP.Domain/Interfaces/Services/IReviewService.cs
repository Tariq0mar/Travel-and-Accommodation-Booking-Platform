using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services;

public interface IReviewService
{
    Task<Review> GetByIdAsync(Guid id);
    Task<IEnumerable<Review>> GetAllAsync(ReviewFilter queryFilter);
    Task<Review> AddAsync(Review review);
    Task UpdateAsync(Review review);
    Task DeleteAsync(Guid id);
}