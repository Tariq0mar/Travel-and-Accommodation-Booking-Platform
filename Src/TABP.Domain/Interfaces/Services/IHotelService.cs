using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services;

public interface IHotelService
{
    Task<Hotel> GetByIdAsync(Guid id);
    Task<IEnumerable<Hotel>> GetAllAsync(HotelFilter queryFilter);
    Task<Hotel> AddAsync(Hotel hotel);
    Task UpdateAsync(Hotel hotel);
    Task DeleteAsync(Guid id);
}