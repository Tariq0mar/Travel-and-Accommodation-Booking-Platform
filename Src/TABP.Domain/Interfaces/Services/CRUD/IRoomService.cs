using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services.CRUD;

public interface IRoomService
{
    Task<Room> GetByIdAsync(int id);
    Task<IEnumerable<Room>> GetAllAsync(RoomFilter queryFilter);
    Task<Room> AddAsync(Room room);
    Task UpdateAsync(Room room);
    Task DeleteAsync(int id);
}