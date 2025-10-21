using FluentValidation;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services;

public class HotelAmenityService : IHotelAmenityService
{
    private readonly IHotelAmenityRepository _hotelAmenityRepository;
    private readonly IValidator<HotelAmenity> _hotelAmenityValidator;

    public HotelAmenityService(
        IHotelAmenityRepository hotelAmenityRepository,
        IValidator<HotelAmenity> hotelAmenityValidator)
    {
        _hotelAmenityRepository = hotelAmenityRepository ?? throw new ArgumentNullException(nameof(hotelAmenityRepository));
        _hotelAmenityValidator = hotelAmenityValidator ?? throw new ArgumentNullException(nameof(hotelAmenityValidator));
    }

    public async Task<HotelAmenity> GetByIdAsync(int id)
    {
        var hotelAmenity = await _hotelAmenityRepository.GetByIdAsync(id);

        if (hotelAmenity is null)
        {
            throw new NotFoundException($"HotelAmenity with Id = {id} not found.");
        }

        return hotelAmenity;
    }

    public async Task<IEnumerable<HotelAmenity>> GetAllAsync(HotelAmenityFilter queryFilter)
    {
        return await _hotelAmenityRepository.GetAllAsync(queryFilter);
    }

    public async Task<HotelAmenity> AddAsync(HotelAmenity hotelAmenity)
    {
        var validation = await _hotelAmenityValidator.ValidateAsync(hotelAmenity);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid hotel amenity: {errors}");
        }

        var addedHotelAmenity = await _hotelAmenityRepository.AddAsync(hotelAmenity);

        if (addedHotelAmenity is null)
        {
            throw new CreationException($"HotelAmenity could not be created.");
        }

        await _hotelAmenityRepository.SaveChangesAsync();

        return addedHotelAmenity;
    }

    public async Task UpdateAsync(HotelAmenity hotelAmenity)
    {
        var validation = await _hotelAmenityValidator.ValidateAsync(hotelAmenity);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid hotel amenity update: {errors}");
        }

        var success = await _hotelAmenityRepository.UpdateAsync(hotelAmenity);
        if (!success)
        {
            throw new NotFoundException($"HotelAmenity with Id = {hotelAmenity.Id} not found.");
        }

        await _hotelAmenityRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var success = await _hotelAmenityRepository.DeleteAsync(id);
        if (!success)
        {
            throw new NotFoundException($"HotelAmenity with Id = {id} not found.");
        }

        await _hotelAmenityRepository.SaveChangesAsync();
    }
}
