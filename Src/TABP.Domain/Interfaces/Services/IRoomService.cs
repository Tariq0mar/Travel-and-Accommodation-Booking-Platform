using TABP.Domain.Entities;
using TABP.Domain.QueryFilters;

namespace TABP.Domain.Interfaces.Services;

public interface IRoomService
{
    Task<Room> GetByIdAsync(Guid id);
    Task<IEnumerable<Room>> GetAllAsync(RoomFilter queryFilter);
    Task<Room> AddAsync(Room room);
    Task UpdateAsync(Room room);
    Task DeleteAsync(Guid id);
}