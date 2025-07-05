using TABP.Domain.Entities;
using TABP.Domain.Filters;

namespace TABP.Domain.Interfaces;

public interface IRoomCategoryDiscountRepository
{
    Task<RoomCategoryDiscount?> GetByIdAsync(Guid id);
    Task<IEnumerable<RoomCategoryDiscount>> GetAllAsync(RoomCategoryDiscountFilter filter);
    Task<RoomCategoryDiscount> AddAsync(RoomCategoryDiscount roomCategoryDiscount);
    Task<bool> UpdateAsync(RoomCategoryDiscount roomCategoryDiscount);
    Task<bool> DeleteAsync(RoomCategoryDiscount roomCategoryDiscount);
}