using TABP.Domain.Entities;
using TABP.Domain.QueryFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface IAmenityRepository
{
    Task<Amenity?> GetByIdAsync(Guid id);
    Task<IEnumerable<Amenity>> GetAllAsync(AmenityFilter filter);
    Task<Amenity> AddAsync(Amenity amenity);
    Task<bool> UpdateAsync(Amenity amenity);
    Task<bool> DeleteAsync(Guid id);
}