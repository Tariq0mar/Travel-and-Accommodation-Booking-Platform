using FluentValidation;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories.CRUD;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services.CRUD;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IValidator<Booking> _bookingValidator;

    public BookingService(
        IBookingRepository bookingRepository,
        IValidator<Booking> bookingValidator)
    {
        _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        _bookingValidator = bookingValidator ?? throw new ArgumentNullException(nameof(bookingValidator));
    }
    public async Task<Booking> GetByIdAsync(int id)
    {
        var booking = await _bookingRepository.GetByIdAsync(id);

        if (booking is null)
        {
            throw new NotFoundException($"Booking with Id = {id} not found.");
        }

        return booking;
    }

    public async Task<IEnumerable<Booking>> GetAllAsync(BookingFilter queryFilter)
    {
        return await _bookingRepository.GetAllAsync(queryFilter);
    }

    public async Task<Booking> AddAsync(Booking booking)
    {
        var validation = await _bookingValidator.ValidateAsync(booking);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid booking: {errors}");
        }

        var addedBooking = await _bookingRepository.AddAsync(booking);

        if (addedBooking is null)
        {
            throw new CreationException($"Booking for user {booking.UserId} could not be created.");
        }

        await _bookingRepository.SaveChangesAsync();

        return addedBooking;
    }

    public async Task UpdateAsync(Booking booking)
    {
        var validation = await _bookingValidator.ValidateAsync(booking);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid booking update: {errors}");
        }

        var success = await _bookingRepository.UpdateAsync(booking);
        if (!success)
        {
            throw new NotFoundException($"Booking with Id = {booking.Id} not found.");
        }

        await _bookingRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var success = await _bookingRepository.DeleteAsync(id);
        if (!success)
        {
            throw new NotFoundException($"Booking with Id = {id} not found.");
        }

        await _bookingRepository.SaveChangesAsync();
    }
}
