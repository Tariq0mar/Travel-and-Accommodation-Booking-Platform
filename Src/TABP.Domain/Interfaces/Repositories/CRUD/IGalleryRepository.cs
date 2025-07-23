using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Repositories.CRUD;

public interface IGalleryRepository
{
    Task<Gallery?> GetByIdAsync(int id);
    Task<IEnumerable<Gallery>> GetAllAsync(GalleryFilter filter);
    Task<Gallery> AddAsync(Gallery gallery);
    Task<bool> UpdateAsync(Gallery gallery);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}