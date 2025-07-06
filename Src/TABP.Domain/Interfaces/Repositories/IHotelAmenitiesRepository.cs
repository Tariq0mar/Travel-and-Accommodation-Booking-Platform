using TABP.Domain.Entities;
using TABP.Domain.QueryFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface IHotelAmenitiesRepository
{
    Task<HotelAmenity?> GetByIdAsync(Guid id);
    Task<IEnumerable<HotelAmenity>> GetAllAsync(HotelAmenityFilter filter);
    Task<HotelAmenity> AddAsync(HotelAmenity hotelAmenities);
    Task<bool> UpdateAsync(HotelAmenity hotelAmenities);
    Task<bool> DeleteAsync(HotelAmenity hotelAmenities);
}