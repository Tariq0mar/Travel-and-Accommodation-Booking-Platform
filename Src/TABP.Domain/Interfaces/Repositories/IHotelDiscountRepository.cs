using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface IHotelDiscountRepository
{
    Task<HotelDiscount?> GetByIdAsync(int id);
    Task<IEnumerable<HotelDiscount>> GetAllAsync(HotelDiscountFilter filter);
    Task<HotelDiscount> AddAsync(HotelDiscount hotelDiscount);
    Task<bool> UpdateAsync(HotelDiscount hotelDiscount);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}