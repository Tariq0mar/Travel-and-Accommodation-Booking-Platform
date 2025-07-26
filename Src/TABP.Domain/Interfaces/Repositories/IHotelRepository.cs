using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface IHotelRepository
{
    Task<Hotel?> GetByIdAsync(int id);
    Task<Hotel?> GetByIdFullDetailsAsync(int id);
    Task<IEnumerable<Hotel>> GetAllAsync(HotelFilter filter);
    Task<Hotel> AddAsync(Hotel hotel);
    Task<bool> UpdateAsync(Hotel hotel);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}