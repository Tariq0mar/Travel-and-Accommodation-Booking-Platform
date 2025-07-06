using TABP.Domain.Entities;
using TABP.Domain.QueryFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id);
    Task<IEnumerable<Booking>> GetAllAsync(BookingFilter filter);
    Task<Booking> AddAsync(Booking booking);
    Task<bool> UpdateAsync(Booking booking);
    Task<bool> DeleteAsync(Guid id);
}