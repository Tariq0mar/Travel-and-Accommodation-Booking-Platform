using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services.CRUD;

public interface ILocationService
{
    Task<Location> GetByIdAsync(int id);
    Task<IEnumerable<Location>> GetAllAsync(LocationFilter queryFilter);
    Task<Location> AddAsync(Location location);
    Task UpdateAsync(Location location);
    Task DeleteAsync(int id);
}