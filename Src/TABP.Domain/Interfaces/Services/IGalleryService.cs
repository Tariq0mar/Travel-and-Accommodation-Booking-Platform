using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services;

public interface IGalleryService
{
    Task<Gallery> GetByIdAsync(Guid id);
    Task<IEnumerable<Gallery>> GetAllAsync(GalleryFilter queryFilter);
    Task<Gallery> AddAsync(Gallery gallery);
    Task UpdateAsync(Gallery gallery);
    Task DeleteAsync(Guid id);
}