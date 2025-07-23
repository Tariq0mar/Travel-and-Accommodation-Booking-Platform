using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services.CRUD;

public interface IRoomCategoryService
{
    Task<RoomCategory> GetByIdAsync(int id);
    Task<IEnumerable<RoomCategory>> GetAllAsync(RoomCategoryFilter queryFilter);
    Task<RoomCategory> AddAsync(RoomCategory roomCategory);
    Task UpdateAsync(RoomCategory roomCategory);
    Task DeleteAsync(int id);
}