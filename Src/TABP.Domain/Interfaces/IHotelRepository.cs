using TABP.Domain.Entities;
using TABP.Domain.Filters;

namespace TABP.Domain.Interfaces;

public interface IHotelRepository
{
    Task<Hotel?> GetByIdAsync(Guid id);
    Task<IEnumerable<Hotel>> GetAllAsync(HotelFilter filter);
    Task<Hotel> AddAsync(Hotel hotel);
    Task<bool> UpdateAsync(Hotel hotel);
    Task<bool> DeleteAsync(Hotel hotel);
}