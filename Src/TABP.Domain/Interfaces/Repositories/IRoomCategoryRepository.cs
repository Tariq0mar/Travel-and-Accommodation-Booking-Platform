using TABP.Domain.Entities;
using TABP.Domain.QueryFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface IRoomCategoryRepository
{
    Task<RoomCategory?> GetByIdAsync(Guid id);
    Task<IEnumerable<RoomCategory>> GetAllAsync(RoomCategoryFilter filter);
    Task<RoomCategory> AddAsync(RoomCategory roomCategory);
    Task<bool> UpdateAsync(RoomCategory roomCategory);
    Task<bool> DeleteAsync(Guid id);
}