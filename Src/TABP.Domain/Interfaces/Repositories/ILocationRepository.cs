using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface ILocationRepository
{
    Task<Location?> GetByIdAsync(int id);
    Task<IEnumerable<Location>> GetAllAsync(LocationFilter filter);
    Task<Location> AddAsync(Location location);
    Task<bool> UpdateAsync(Location location);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}