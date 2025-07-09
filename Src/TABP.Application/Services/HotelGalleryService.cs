using FluentValidation;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services;

public class HotelGalleryService : IHotelGalleryService
{
    private readonly IHotelGalleryRepository _hotelGalleryRepository;
    private readonly IValidator<HotelGallery> _hotelGalleryValidator;

    public HotelGalleryService(
        IHotelGalleryRepository hotelGalleryRepository,
        IValidator<HotelGallery> hotelGalleryValidator)
    {
        _hotelGalleryRepository = hotelGalleryRepository ?? throw new ArgumentNullException(nameof(hotelGalleryRepository));
        _hotelGalleryValidator = hotelGalleryValidator ?? throw new ArgumentNullException(nameof(hotelGalleryValidator));
    }

    public async Task<HotelGallery> GetByIdAsync(int id)
    {
        var hotelGallery = await _hotelGalleryRepository.GetByIdAsync(id);

        if (hotelGallery is null)
        {
            throw new NotFoundException($"HotelGallery with Id = {id} not found.");
        }

        return hotelGallery;
    }

    public async Task<IEnumerable<HotelGallery>> GetAllAsync(HotelGalleryFilter queryFilter)
    {
        return await _hotelGalleryRepository.GetAllAsync(queryFilter);
    }

    public async Task<HotelGallery> AddAsync(HotelGallery hotelGallery)
    {
        var validation = await _hotelGalleryValidator.ValidateAsync(hotelGallery);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid hotel gallery: {errors}");
        }

        var addedHotelGallery = await _hotelGalleryRepository.AddAsync(hotelGallery);

        if (addedHotelGallery is null)
        {
            throw new CreationException($"HotelGallery could not be created.");
        }

        await _hotelGalleryRepository.SaveChangesAsync();

        return addedHotelGallery;
    }

    public async Task UpdateAsync(HotelGallery hotelGallery)
    {
        var validation = await _hotelGalleryValidator.ValidateAsync(hotelGallery);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid hotel gallery update: {errors}");
        }

        var success = await _hotelGalleryRepository.UpdateAsync(hotelGallery);
        if (!success)
        {
            throw new NotFoundException($"HotelGallery with Id = {hotelGallery.Id} not found.");
        }

        await _hotelGalleryRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var success = await _hotelGalleryRepository.DeleteAsync(id);
        if (!success)
        {
            throw new NotFoundException($"HotelGallery with Id = {id} not found.");
        }

        await _hotelGalleryRepository.SaveChangesAsync();
    }
}
