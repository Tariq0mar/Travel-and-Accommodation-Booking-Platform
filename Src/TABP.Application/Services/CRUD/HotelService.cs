using FluentValidation;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories.CRUD;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services.CRUD;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IValidator<Hotel> _hotelValidator;

    public HotelService(
        IHotelRepository hotelRepository,
        IValidator<Hotel> hotelValidator)
    {
        _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
        _hotelValidator = hotelValidator ?? throw new ArgumentNullException(nameof(hotelValidator));
    }

    public async Task<Hotel> GetByIdAsync(int id)
    {
        var hotel = await _hotelRepository.GetByIdAsync(id);

        if (hotel is null)
        {
            throw new NotFoundException($"Hotel with Id = {id} not found.");
        }

        return hotel;
    }

    public async Task<IEnumerable<Hotel>> GetAllAsync(HotelFilter queryFilter)
    {
        return await _hotelRepository.GetAllAsync(queryFilter);
    }

    public async Task<Hotel> AddAsync(Hotel hotel)
    {
        var validation = await _hotelValidator.ValidateAsync(hotel);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid hotel: {errors}");
        }

        var addedHotel = await _hotelRepository.AddAsync(hotel);

        if (addedHotel is null)
        {
            throw new CreationException($"Hotel could not be created.");
        }

        await _hotelRepository.SaveChangesAsync();

        return addedHotel;
    }

    public async Task UpdateAsync(Hotel hotel)
    {
        var validation = await _hotelValidator.ValidateAsync(hotel);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid hotel update: {errors}");
        }

        var success = await _hotelRepository.UpdateAsync(hotel);
        if (!success)
        {
            throw new NotFoundException($"Hotel with Id = {hotel.Id} not found.");
        }

        await _hotelRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var success = await _hotelRepository.DeleteAsync(id);
        if (!success)
        {
            throw new NotFoundException($"Hotel with Id = {id} not found.");
        }

        await _hotelRepository.SaveChangesAsync();
    }
}
