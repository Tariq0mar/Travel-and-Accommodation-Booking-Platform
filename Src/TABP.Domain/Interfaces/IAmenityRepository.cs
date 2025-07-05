using TABP.Domain.Entities;
using TABP.Domain.Filters;

namespace TABP.Domain.Interfaces;

public interface IAmenityRepository
{
    Task<Amenity?> GetByIdAsync(Guid id);
    Task<IEnumerable<Amenity>> GetAllAsync(AmenityFilter filter);
    Task<Amenity> AddAsync(Amenity amenity);
    Task<bool> UpdateAsync(Amenity amenity);
    Task<bool> DeleteAsync(Amenity amenity);
}