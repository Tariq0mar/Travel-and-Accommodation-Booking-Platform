using TABP.Domain.Entities;
using TABP.Domain.Filters;

namespace TABP.Domain.Interfaces;

public interface IHotelAmenitiesRepository
{
    Task<HotelAmenity?> GetByIdAsync(Guid id);
    Task<IEnumerable<HotelAmenity>> GetAllAsync(HotelAmenityFilter filter);
    Task<HotelAmenity> AddAsync(HotelAmenity hotelAmenities);
    Task<bool> UpdateAsync(HotelAmenity hotelAmenities);
    Task<bool> DeleteAsync(HotelAmenity hotelAmenities);
}