using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface IHotelRepository
{
    Task<Hotel?> GetByIdAsync(Guid id);
    Task<IEnumerable<Hotel>> GetAllAsync(HotelFilter filter);
    Task<Hotel> AddAsync(Hotel hotel);
    Task<bool> UpdateAsync(Hotel hotel);
    Task<bool> DeleteAsync(Guid id);
}