using TABP.Domain.Entities;
using TABP.Domain.QueryFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface IRoomAmenityRepository
{
    Task<RoomAmenity?> GetByIdAsync(Guid id);
    Task<IEnumerable<RoomAmenity>> GetAllAsync(RoomAmenityFilter filter);
    Task<RoomAmenity> AddAsync(RoomAmenity booking);
    Task<bool> UpdateAsync(RoomAmenity booking);
    Task<bool> DeleteAsync(RoomAmenity booking);
}