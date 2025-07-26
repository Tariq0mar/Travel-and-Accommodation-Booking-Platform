using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services;

public interface IHotelAmenityService
{
    Task<HotelAmenity> GetByIdAsync(int id);
    Task<IEnumerable<HotelAmenity>> GetAllAsync(HotelAmenityFilter queryFilter);
    Task<HotelAmenity> AddAsync(HotelAmenity hotelAmenity);
    Task UpdateAsync(HotelAmenity hotelAmenity);
    Task DeleteAsync(int id);
}