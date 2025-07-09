using TABP.Domain.Entities;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<Booking> GetByIdAsync(Guid id)
    {
        var booking = await _bookingRepository.GetByIdAsync(id);

        if (booking is null)
        {
            throw new NotFoundException($"Booking With Id = {id}");
        }

        return booking;
    }

    public Task<IEnumerable<Booking>> GetAllAsync(BookingFilter queryFilter)
    {
        throw new NotImplementedException();
    }

    public async Task<Booking> AddAsync(Booking booking)
    {

    }

    public Task UpdateAsync(Booking booking)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}