using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Repositories.CRUD;

public interface IHotelAmenityRepository
{
    Task<HotelAmenity?> GetByIdAsync(int id);
    Task<IEnumerable<HotelAmenity>> GetAllAsync(HotelAmenityFilter filter);
    Task<HotelAmenity> AddAsync(HotelAmenity hotelAmenities);
    Task<bool> UpdateAsync(HotelAmenity hotelAmenities);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}