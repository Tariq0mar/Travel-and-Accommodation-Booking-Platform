using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services.CRUD;

public interface IGalleryService
{
    Task<Gallery> GetByIdAsync(int id);
    Task<IEnumerable<Gallery>> GetAllAsync(GalleryFilter queryFilter);
    Task<Gallery> AddAsync(Gallery gallery);
    Task UpdateAsync(Gallery gallery);
    Task DeleteAsync(int id);
}