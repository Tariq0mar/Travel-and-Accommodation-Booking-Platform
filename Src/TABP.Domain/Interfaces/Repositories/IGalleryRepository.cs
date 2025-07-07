using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface IGalleryRepository
{
    Task<Gallery?> GetByIdAsync(Guid id);
    Task<IEnumerable<Gallery>> GetAllAsync(GalleryFilter filter);
    Task<Gallery> AddAsync(Gallery gallery);
    Task<bool> UpdateAsync(Gallery gallery);
    Task<bool> DeleteAsync(Guid id);
    Task SaveChangesAsync();
}