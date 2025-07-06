using TABP.Domain.Entities;
using TABP.Domain.QueryFilters;

namespace TABP.Domain.Interfaces.Services;

public interface IBookingService
{
    Task<Booking> GetByIdAsync(Guid id);
    Task<IEnumerable<Booking>> GetAllAsync(BookingFilter queryFilter);
    Task<Booking> AddAsync(Booking booking);
    Task UpdateAsync(Booking booking);
    Task DeleteAsync(Guid id);
}