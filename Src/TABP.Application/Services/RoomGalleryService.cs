using FluentValidation;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services;

public class RoomGalleryService : IRoomGalleryService
{
    private readonly IRoomGalleryRepository _roomGalleryRepository;
    private readonly IValidator<RoomGallery> _roomGalleryValidator;

    public RoomGalleryService(
        IRoomGalleryRepository roomGalleryRepository,
        IValidator<RoomGallery> roomGalleryValidator)
    {
        _roomGalleryRepository = roomGalleryRepository ?? throw new ArgumentNullException(nameof(roomGalleryRepository));
        _roomGalleryValidator = roomGalleryValidator ?? throw new ArgumentNullException(nameof(roomGalleryValidator));
    }

    public async Task<RoomGallery> GetByIdAsync(int id)
    {
        var roomGallery = await _roomGalleryRepository.GetByIdAsync(id);

        if (roomGallery is null)
        {
            throw new NotFoundException($"RoomGallery with Id = {id} not found.");
        }

        return roomGallery;
    }

    public async Task<IEnumerable<RoomGallery>> GetAllAsync(RoomGalleryFilter queryFilter)
    {
        return await _roomGalleryRepository.GetAllAsync(queryFilter);
    }

    public async Task<RoomGallery> AddAsync(RoomGallery roomGallery)
    {
        var validation = await _roomGalleryValidator.ValidateAsync(roomGallery);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid room gallery: {errors}");
        }

        var addedRoomGallery = await _roomGalleryRepository.AddAsync(roomGallery);

        if (addedRoomGallery is null)
        {
            throw new CreationException($"RoomGallery could not be created.");
        }

        await _roomGalleryRepository.SaveChangesAsync();

        return addedRoomGallery;
    }

    public async Task UpdateAsync(RoomGallery roomGallery)
    {
        var validation = await _roomGalleryValidator.ValidateAsync(roomGallery);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid room gallery update: {errors}");
        }

        var success = await _roomGalleryRepository.UpdateAsync(roomGallery);
        if (!success)
        {
            throw new NotFoundException($"RoomGallery with Id = {roomGallery.Id} not found.");
        }

        await _roomGalleryRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var success = await _roomGalleryRepository.DeleteAsync(id);
        if (!success)
        {
            throw new NotFoundException($"RoomGallery with Id = {id} not found.");
        }

        await _roomGalleryRepository.SaveChangesAsync();
    }
}
