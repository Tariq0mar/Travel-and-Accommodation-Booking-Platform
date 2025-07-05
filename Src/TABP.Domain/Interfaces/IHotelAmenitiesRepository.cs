using TABP.Domain.Entities;
using TABP.Domain.Filters;

namespace TABP.Domain.Interfaces;

public interface IHotelAmenitiesRepository
{
    Task<HotelAmenities?> GetByIdAsync(Guid id);
    Task<IEnumerable<HotelAmenities>> GetAllAsync(HotelAmenitiesFilter filter);
    Task<HotelAmenities> AddAsync(HotelAmenities hotelAmenities);
    Task<bool> UpdateAsync(HotelAmenities hotelAmenities);
    Task<bool> DeleteAsync(HotelAmenities hotelAmenities);
}