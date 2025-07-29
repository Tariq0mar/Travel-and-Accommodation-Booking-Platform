using FluentValidation;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services;

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
<<<<<<< Updated upstream
=======

    public async Task BookRoomAndAddToCartAsync(BookingRoomModel model)
    {
        await _bookingRepository.BeginTransactionAsync();

        if (model.StartDate >= model.EndDate)
            throw new BadRequestException("StartDate must be before EndDate.");

        if (model.AdultsCount is 0 && model.ChildrenCount is 0)
            throw new BadRequestException("At least one adult or child must be specified.");

        
        var isAvailable = await _bookingRepository.IsRoomAvailableAsync(model.RoomId, model.StartDate, model.EndDate);
        if (!isAvailable)
            throw new InvalidOperationException("Room is already booked for the selected dates.");

        var booking = new Booking
        {
            UserId = model.UserId,
            RoomId = model.RoomId,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            AdultsCount = model.AdultsCount,
            ChildrenCount = model.ChildrenCount,
            Currency = model.Currency,
            PriceWithoutDiscount = model.PriceWithoutDiscount,
            PriceWithDiscount = model.PriceWithDiscount,
            CreatedAt = DateTime.UtcNow,
        };

        await _bookingRepository.AddAsync(booking);

        var cartItem = await _cartItemRepository.GetUserRoomItemAsync(model.UserId, model.RoomId);
        if (cartItem is not null)
        {
            cartItem.Quantity += 1;
            await _cartItemRepository.UpdateAsync(cartItem);
        }
        else
        {
            var newCartItem = new CartItem
            {
                UserId = model.UserId,
                RoomId = model.RoomId,
                Quantity = 1,
                BookingConfirmed = true,
                PaymentCompleted = false,
                CreatedAt = DateTime.UtcNow
            };
            await _cartItemRepository.AddAsync(newCartItem);
        }

        await _bookingRepository.CommitTransactionAsync();
    }

>>>>>>> Stashed changes
}
