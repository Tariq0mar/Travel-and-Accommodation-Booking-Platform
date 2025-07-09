using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface IAmenityRepository
{
    Task<Amenity?> GetByIdAsync(int id);
    Task<IEnumerable<Amenity>> GetAllAsync(AmenityFilter filter);
    Task<Amenity> AddAsync(Amenity amenity);
    Task<bool> UpdateAsync(Amenity amenity);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}