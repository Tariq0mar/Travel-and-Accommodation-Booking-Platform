using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services.CRUD;

public interface IAmenityService
{
    Task<Amenity> GetByIdAsync(int id);
    Task<IEnumerable<Amenity>> GetAllAsync(AmenityFilter queryFilter);
    Task<Amenity> AddAsync(Amenity amenity);
    Task UpdateAsync(Amenity amenity);
    Task DeleteAsync(int id);
}