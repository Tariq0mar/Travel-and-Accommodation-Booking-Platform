using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface IRoomAmenityRepository
{
    Task<RoomAmenity?> GetByIdAsync(int id);
    Task<IEnumerable<RoomAmenity>> GetAllAsync(RoomAmenityFilter filter);
    Task<RoomAmenity> AddAsync(RoomAmenity booking);
    Task<bool> UpdateAsync(RoomAmenity booking);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}