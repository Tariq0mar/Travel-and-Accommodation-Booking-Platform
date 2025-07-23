using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Repositories.CRUD;

public interface IRoomCategoryDiscountRepository
{
    Task<RoomCategoryDiscount?> GetByIdAsync(int id);
    Task<IEnumerable<RoomCategoryDiscount>> GetAllAsync(RoomCategoryDiscountFilter filter);
    Task<RoomCategoryDiscount> AddAsync(RoomCategoryDiscount roomCategoryDiscount);
    Task<bool> UpdateAsync(RoomCategoryDiscount roomCategoryDiscount);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}