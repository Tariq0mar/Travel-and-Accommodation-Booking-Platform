using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services;

public interface ILocationService
{
    Task<Location> GetByIdAsync(Guid id);
    Task<IEnumerable<Location>> GetAllAsync(LocationFilter queryFilter);
    Task<Location> AddAsync(Location location);
    Task UpdateAsync(Location location);
    Task DeleteAsync(Guid id);
}