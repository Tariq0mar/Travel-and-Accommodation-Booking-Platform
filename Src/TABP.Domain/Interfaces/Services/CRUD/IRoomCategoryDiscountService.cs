using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services.CRUD;

public interface IRoomCategoryDiscountService
{
    Task<RoomCategoryDiscount> GetByIdAsync(int id);
    Task<IEnumerable<RoomCategoryDiscount>> GetAllAsync(RoomCategoryDiscountFilter queryFilter);
    Task<RoomCategoryDiscount> AddAsync(RoomCategoryDiscount roomCategoryDiscount);
    Task UpdateAsync(RoomCategoryDiscount roomCategoryDiscountoomCategoryDiscount);
    Task DeleteAsync(int id);
}