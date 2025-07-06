using TABP.Domain.Entities;
using TABP.Domain.Filters;

namespace TABP.Domain.Interfaces;

public interface IRoomAmenitiesRepository
{
    Task<RoomAmenity?> GetByIdAsync(Guid id);
    Task<IEnumerable<RoomAmenity>> GetAllAsync(RoomAmenityFilter filter);
    Task<RoomAmenity> AddAsync(RoomAmenity booking);
    Task<bool> UpdateAsync(RoomAmenity booking);
    Task<bool> DeleteAsync(RoomAmenity booking);
}