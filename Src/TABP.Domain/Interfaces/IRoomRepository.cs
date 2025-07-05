using TABP.Domain.Entities;
using TABP.Domain.Filters;

namespace TABP.Domain.Interfaces;

public interface IRoomRepository
{
    Task<Room?> GetByIdAsync(Guid id);
    Task<IEnumerable<Room>> GetAllAsync(RoomFilter filter);
    Task<Room> AddAsync(Room room);
    Task<bool> UpdateAsync(Room room);
    Task<bool> DeleteAsync(Room room);
}