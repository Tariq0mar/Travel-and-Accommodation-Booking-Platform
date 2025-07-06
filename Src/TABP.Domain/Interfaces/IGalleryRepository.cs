using TABP.Domain.Entities;
using TABP.Domain.Filters;

namespace TABP.Domain.Interfaces;

public interface IGalleryRepository
{
    Task<Gallery?> GetByIdAsync(Guid id);
    Task<IEnumerable<Gallery>> GetAllAsync(GalleryFilter filter);
    Task<Gallery> AddAsync(Gallery gallery);
    Task<bool> UpdateAsync(Gallery gallery);
    Task<bool> DeleteAsync(Gallery gallery);
}