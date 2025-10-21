using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services;

public interface IRoomAmenityService
{
    Task<RoomAmenity> GetByIdAsync(int id);
    Task<IEnumerable<RoomAmenity>> GetAllAsync(RoomAmenityFilter queryFilter);
    Task<RoomAmenity> AddAsync(RoomAmenity roomAmenity);
    Task UpdateAsync(RoomAmenity roomAmenity);
    Task DeleteAsync(int id);
}