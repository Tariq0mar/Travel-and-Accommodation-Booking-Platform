using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface IRoomRepository
{
    Task<Room?> GetByIdAsync(int id);
    Task<IEnumerable<Room>> GetAllAsync(RoomFilter filter);
    Task<Room> AddAsync(Room room);
    Task<bool> UpdateAsync(Room room);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}