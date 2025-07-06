using TABP.Domain.Entities;
using TABP.Domain.QueryFilters;

namespace TABP.Domain.Interfaces.Services;

public interface IAmenityService
{
    Task<Amenity> GetByIdAsync(Guid id);
    Task<IEnumerable<Amenity>> GetAllAsync(AmenityFilter queryFilter);
    Task<Amenity> AddAsync(Amenity amenity);
    Task UpdateAsync(Amenity amenity);
    Task DeleteAsync(Guid id);
}