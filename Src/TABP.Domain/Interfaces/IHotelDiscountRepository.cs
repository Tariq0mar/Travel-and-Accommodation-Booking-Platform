using TABP.Domain.Entities;
using TABP.Domain.Filters;

namespace TABP.Domain.Interfaces;

public interface IHotelDiscountRepository
{
    Task<HotelDiscount?> GetByIdAsync(Guid id);
    Task<IEnumerable<HotelDiscount>> GetAllAsync(HotelDiscountFilter filter);
    Task<HotelDiscount> AddAsync(HotelDiscount hotelDiscount);
    Task<bool> UpdateAsync(HotelDiscount hotelDiscount);
    Task<bool> DeleteAsync(HotelDiscount hotelDiscount);
}