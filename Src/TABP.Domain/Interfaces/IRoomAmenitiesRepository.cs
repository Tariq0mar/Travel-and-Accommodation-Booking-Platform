using TABP.Domain.Entities;
using TABP.Domain.Filters;

namespace TABP.Domain.Interfaces;

public interface IRoomAmenitiesRepository
{
    Task<RoomAmenities?> GetByIdAsync(Guid id);
    Task<IEnumerable<RoomAmenities>> GetAllAsync(RoomAmenitiesFilter filter);
    Task<RoomAmenities> AddAsync(RoomAmenities booking);
    Task<bool> UpdateAsync(RoomAmenities booking);
    Task<bool> DeleteAsync(RoomAmenities booking);
}