using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services;

public interface IHotelDiscountService
{
    Task<HotelDiscount> GetByIdAsync(Guid id);
    Task<IEnumerable<HotelDiscount>> GetAllAsync(HotelDiscountFilter queryFilter);
    Task<HotelDiscount> AddAsync(HotelDiscount hotelDiscount);
    Task UpdateAsync(HotelDiscount hotelDiscount);
    Task DeleteAsync(Guid id);
}