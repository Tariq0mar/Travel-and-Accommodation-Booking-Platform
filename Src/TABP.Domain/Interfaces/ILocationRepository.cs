using TABP.Domain.Entities;
using TABP.Domain.Filters;

namespace TABP.Domain.Interfaces;

public interface ILocationRepository
{
    Task<Location?> GetByIdAsync(Guid id);
    Task<IEnumerable<Location>> GetAllAsync(LocationFilter filter);
    Task<Location> AddAsync(Location location);
    Task<bool> UpdateAsync(Location location);
    Task<bool> DeleteAsync(Location location);
}