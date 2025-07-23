using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services.CRUD;

public interface IHotelDiscountService
{
    Task<HotelDiscount> GetByIdAsync(int id);
    Task<IEnumerable<HotelDiscount>> GetAllAsync(HotelDiscountFilter queryFilter);
    Task<HotelDiscount> AddAsync(HotelDiscount hotelDiscount);
    Task UpdateAsync(HotelDiscount hotelDiscount);
    Task DeleteAsync(int id);
}