using TABP.Domain.Entities;
using TABP.Domain.Models.BookingRoom;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services;

public interface IBookingService
{
    Task<Booking> GetByIdAsync(int id);
    Task<IEnumerable<Booking>> GetAllAsync(BookingFilter queryFilter);
    Task<Booking> AddAsync(Booking booking);
    Task UpdateAsync(Booking booking);
    Task DeleteAsync(int id);
    Task BookRoomAndAddToCartAsync(BookingRoomModel bookingRoomModel);
}