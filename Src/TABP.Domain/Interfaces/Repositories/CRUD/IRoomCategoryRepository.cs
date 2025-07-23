using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Repositories.CRUD;

public interface IRoomCategoryRepository
{
    Task<RoomCategory?> GetByIdAsync(int id);
    Task<IEnumerable<RoomCategory>> GetAllAsync(RoomCategoryFilter filter);
    Task<RoomCategory> AddAsync(RoomCategory roomCategory);
    Task<bool> UpdateAsync(RoomCategory roomCategory);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}