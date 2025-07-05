using TABP.Domain.Entities;
using TABP.Domain.Filters;

namespace TABP.Domain.Interfaces;

public interface IRoomCategoryRepository
{
    Task<RoomCategory?> GetByIdAsync(Guid id);
    Task<IEnumerable<RoomCategory>> GetAllAsync(RoomCategoryFilter filter);
    Task<RoomCategory> AddAsync(RoomCategory roomCategory);
    Task<bool> UpdateAsync(RoomCategory roomCategory);
    Task<bool> DeleteAsync(RoomCategory roomCategory);
}